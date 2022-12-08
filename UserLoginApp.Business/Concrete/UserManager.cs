using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MongoDB.Bson;
using System.Security.Claims;
using UserLoginApp.Business.Helper.Base64;
using UserLoginApp.Business.Helper.Generators;
using UserLoginApp.Business.Helper.JwtToken;
using UserLoginApp.Business.Helper.Mailler;
using UserLoginApp.Business.Helper.Mailler.Model;
using UserLoginApp.Business.Interfaces;
using UserLoginApp.DataAccess.Interfaces;
using UserLoginApp.DTO.DTOs.UserDTOs;
using UserLoginApp.Entities.Concrete;
using UserLoginApp.Entities.Concrete.Reponse;
using UserLoginApp.Entities.Conrete;

namespace UserLoginApp.Business.Concrete
{
    public class UserManager : GenericManagerMD<User>, IUserService
    {
        private readonly IGenericRepositoryMD<User> _genericRepositoryMD;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IEMailService _eMailService;
        private string _generatedToken = null;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMemoryCache _memoryCache;


        public UserManager(IMemoryCache memoryCache,IHttpContextAccessor httpContextAccessor, IGenericRepositoryMD<User> genericRepositoryMD, IConfiguration configuration, ITokenService tokenService, IEMailService eMailService) : base(genericRepositoryMD)
        {
            _genericRepositoryMD = genericRepositoryMD;
            _tokenService = tokenService;
            _config = configuration;
            _eMailService = eMailService;
            _httpContextAccessor = httpContextAccessor;
            _memoryCache = memoryCache;
        }

        public Token Login(LoginDto loginDto)
        {
            var _validUser = _genericRepositoryMD.Get(x => x.Username == loginDto.Username && x.Password == loginDto.Password);
            var _retData = new Token();



            if (_validUser != null)
            {
                if (_validUser.IsConfirm)
                {

                    var claims = new List<Claim> {
                    new Claim("Username", _validUser.Username),
                    new Claim("UserId",_validUser.Id.ToString()),
                    new Claim("Email",_validUser.Email.ToString()),
                    new Claim("LoginDate",DateTime.Now.ToString()),

                    };
                    claims.Add(new Claim(ClaimTypes.Role, _validUser.Role));


                    _generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(),
                        _config["Jwt:Issuer"].ToString(), _validUser, claims);

                    if (_generatedToken != null)
                    {



                        _retData.Id = _validUser.Id;
                        _retData.Username = _validUser.Username;
                        _retData.Error = false;
                        _retData.Message = "Token Oluşturuldu";
                        _retData.AccessToken = _generatedToken;
                        _retData.Role = _validUser.Role;


                    }
                    else
                    {
                        _retData.Error = true;
                        _retData.Message = "Token Oluşturulurken Hata Meydana Geldi.";
                    }
                }
                else
                {
                    _retData.Error = true;
                    _retData.Message = "Onaysız Hesap.";
                }

            }
            else
            {
                _retData.Error = true;
                _retData.Message = "Kullanıcı bulunamadı.";
            }
            return _retData;
        }

        public User Register(User user)
        {

            try
            {
                _genericRepositoryMD.Add(user);
                var _registeredUser = _genericRepositoryMD.Get(x => x.Id == user.Id);

                _eMailService.SendEmailAsync(new EMailModel
                {
                    To = user.Email,
                    Subject = "Kayıt Başarılı!",
                    Body = @$"<h3>Kayıt Onayı</h3></br>
                            Merhaba <b>{user.Username},</b></br>
                            Onay Anahtarı: {Base64Code.Base64Encode(user.Id.ToString())}"
                });
                return _registeredUser;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ConfirmMailResponse ConfirmUser(string id)
        {

            try
            {
                var _registeredUser = _genericRepositoryMD.Get(x => x.Id == Base64Code.Base64Decode(id));
                if (_registeredUser != null)
                {
                    _registeredUser.IsConfirm = true;
                    _registeredUser.ConfirmDate = DateTime.Now;
                    _genericRepositoryMD.Update(_registeredUser);
                    return new ConfirmMailResponse
                    {
                        Username = _registeredUser.Username,
                        Email = _registeredUser.Email,
                        Error = false,
                        Id = _registeredUser.Id,
                        Message = "Hesap Doğrulandı.Giriş yapabilirsiniz.",
                        Password = _registeredUser.Password,
                        Role = _registeredUser.Role,
                        IsConfirm = true,
                    };
                }
                return new ConfirmMailResponse
                {
                    Error = true,
                    Message = "Hatalı Anahtar.",
                    Role = null
                };


            }
            catch (Exception)
            {
                return new ConfirmMailResponse
                {
                    Error = true,
                    Message = "Kullanıcı Bulunamadı",
                    Role = null
                };
            }
        }

        public ForgotPasswordResponse ForgotPassword(ForgotPasswordDto forgotPasswordDto)
        {
            var _resp = new ForgotPasswordResponse
            {
                Error = true,
                Message = "Hata"
            };
            
            try
            {
                var _registeredUser = _genericRepositoryMD.Get(x => x.Username == forgotPasswordDto.Username && x.Email == forgotPasswordDto.Email);
                if (_registeredUser != null)
                {
                    try
                    {
                        var _generatedPasswordCode = PasswordGenerator.GenerateNewPasswordCode(6);
                        _memoryCache.Set($"new_pw_code_{forgotPasswordDto.Username}", _generatedPasswordCode, new MemoryCacheEntryOptions
                        {
                            AbsoluteExpiration = DateTime.Now.AddSeconds(60),
                            Priority = CacheItemPriority.Normal
                        });
                        _eMailService.SendEmailAsync(new EMailModel
                        {
                            To = _registeredUser.Email,
                            Subject = "Şifremi Unuttum",
                            Body = @$"<h3>Yeni Şifre</h3></br>
                            Merhaba <b>{_registeredUser.Username},</b></br>
                            Şifre Değiştirme Kodu: {_generatedPasswordCode} (60 saniye süreli)"
                        });
                        _resp.Error = false;
                        _resp.Message = "Şifre sıfırlama kodu maile gonderıldı";

                    }
                    catch (Exception)
                    {
                        throw;
                    }
                }
                return _resp;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public NewPasswordResponse NewPassword(NewPasswordDto newPasswordDto)
        {
            var _resp=new NewPasswordResponse();
            _resp.Error = true;
            _resp.Message = "Hata";


            var _code=_memoryCache.Get($"new_pw_code_{newPasswordDto.Username}");
            var _user = _genericRepositoryMD.Get(x => x.Username == newPasswordDto.Username);


            if (_code!=null&&_user!=null)
            {
                _user.Password = newPasswordDto.NewPassword;
                try
                {
                    _genericRepositoryMD.Update(_user);
                    _resp.Error = false;
                    _resp.Message = "Başarılı";
                }
                catch (Exception)
                {

                    throw;
                }
            }

            return _resp;
        }

        public IEnumerable<User> GetRegisteredUsers()
        {
            return _genericRepositoryMD.GetAllByFilter(x=>x.IsConfirm);
        }
        public IEnumerable<User> GetRegisteredUsersByDay(int day)
        {
            return _genericRepositoryMD.GetAllByFilter(x => x.IsConfirm==true).Where(x=>(ObjectId.Parse(x.Id).CreationTime) > DateTime.Now.AddDays(-day));
        }

        public IEnumerable<User> GetNotConfirmedUsersByDay(int day)
        {
            return _genericRepositoryMD.GetAllByFilter(x => x.IsConfirm == false).Where(x => (ObjectId.Parse(x.Id).CreationTime).AddDays(+day)<x.ConfirmDate);
        }
    }
}
