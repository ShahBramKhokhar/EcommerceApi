using WebRexErpAPI.Common.CommonDto;
using WebRexErpAPI.Data;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Models;
using WebRexErpAPI.Services.Account.Dto;
using WebRexErpAPI.Services.Common;
using WebRexErpAPI.Services.QuickBase;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;



namespace WebRexErpAPI.Authentication
{
    public class JwtAuthenticationManager : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQuickBaseService _quickBaseService;

      
        public JwtAuthenticationManager(
            IConfiguration configuration,
            IUnitOfWork unitOfWork,
            IHttpContextAccessor httpContextAccessor,
            IQuickBaseService quickBaseService
           
            )
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _quickBaseService = quickBaseService;
        }


       public async Task<UserVM?> Register(UserRegisterDto request)
        {
               
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
                var user = new WebRexErpAPI.Models.User();
                user.Email = request.Email;
                user.FullName = request.FullName;
                user.BusinessName = request.BusinessName;
                user.PhoneNumber = request.PhoneNumber;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;


            //  var customerQBDto = new CustomerQBDto();
            //customerQBDto = await _quickBaseService.FindCustomerQBBusiness(user.BusinessName);
            //var qbCustomer = new CustomerQBDto()
            //{
            //    phoneNumber = request.PhoneNumber,
            //    CustomerName = request.BusinessName,

            //};


            // if(customerQBDto == null || customerQBDto.QBId < 1)
            //  {
            //   customerQBDto = await _quickBaseService.CreateQbCustomer(qbCustomer);
            //  }


            //if (customerQBDto != null && customerQBDto.QBId > 0)
            //{
            //    user.CustomerKeyQB = customerQBDto.QBId;
            //    user.CustomerNameQB = customerQBDto.CustomerName;
            //    user.CustomerQBId = customerQBDto.CustomerId;

            //var contactQBDto = new ContactQBDto()
            //{
            //    ContactName = request.BusinessName,
            //    ContactTitle = request.FullName,
            //    PhoneNumber = request.PhoneNumber,
            //    Email = request.Email,
            //    QBId = 0,
            //    CustomerRecordId = customerQBDto.QBId,
            //    IsCreateCustomer = true,

            //};

            //var contact = new ContactQBDto();
            //contact = await _quickBaseService.FindCustomerContacts(contactQBDto);
            //if (contact.QBId == 0)
            //{
            //    contact = await _quickBaseService.CreateQBContact(contactQBDto);
            //}


            //    }
            //await _context.tblUser.AddAsync(user);
            await _unitOfWork.userRepository.Add(user);
                //await _context.SaveChangesAsync();
                await _unitOfWork.CompleteAsync();

                if (user.Id > 0)
                {
                    var userLogin = new UserDto();
                    userLogin.Email = request.Email;
                    userLogin.Password = request.Password;
                    return await Login(userLogin);
                }

                else
                {
                    return null;
                }
            
           
           
        }

        public async Task<UserVM?> Login(UserDto request)
        {

            var userVM = new UserVM();
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
            if (!_unitOfWork.userRepository.Any(u => u.Email == request.Email))
            {
                return null;
            }

             user = _unitOfWork.userRepository.GetFirstOrDefault(u => u.Email == request.Email);

            // check user not exist 
            //if (user != null || user.Id == 0)
            //{
            //    return null;
            //}
            if (!VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            return await UserToUserVMAndSetTokenAsync(userVM, user);

        }

        public async Task<UserVM?> UserToUserVMAndSetTokenAsync(UserVM userVM, User? user)
        {
            try
            {
                string token = "";
                if (user.Id > 0 && user.Email != "")
                {
                    token = CreateToken(user);
                }

                var customerQBDto = await _quickBaseService.FindCustomerQBBusiness(user.BusinessName);
                if (customerQBDto != null && customerQBDto.QBId > 0)
                {
                    userVM.CustomerKeyQB = customerQBDto.QBId;
                    userVM.CustomerNameQB = customerQBDto.CustomerName;
                    userVM.CustomerQBId = customerQBDto.CustomerId;
                }

                if (token != "")
                {
                    userVM.AccessToken = token;
                    userVM.Email = user.Email;
                    userVM.FullName = user.FullName;
                    userVM.BusinessName = user.BusinessName;
                    userVM.PhoneNumber = user.PhoneNumber;
                    userVM.Id = user.Id;
                    userVM.Examet = user.Examet;
                    userVM.FileName = user.FileName;
                    userVM.FileUrl = user.FileUrl;
                    

                    return userVM;

                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string? Authenticate(string username, string password)
        {
           
            
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, username)
                }),
               
                Expires = DateTime.UtcNow.AddDays(7),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature) 
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public string GetUserEmailClaime()
        {
            var result = string.Empty;
            if (_httpContextAccessor.HttpContext != null)
            {
                result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Name);
            }

            return result;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, "Vistor")
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(
                _configuration.GetSection("Jwt:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }


        bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        public void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private RefreshToken GenerateRefreshToken()
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.Now.AddDays(7),
                Created = DateTime.Now
            };

            return refreshToken;
        }

        private void SetRefreshToken(RefreshToken newRefreshToken)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = newRefreshToken.Expires
            };
            //Response.Cookies.Append("refreshToken", newRefreshToken.Token, cookieOptions);

            //user.RefreshToken = newRefreshToken.Token;
            //user.TokenCreated = newRefreshToken.Created;
            //user.TokenExpires = newRefreshToken.Expires;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
                

            }
        }
    }
}
