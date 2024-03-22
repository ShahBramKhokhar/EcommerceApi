using WebRexErpAPI.Authentication;
using WebRexErpAPI.Business.Email;
using WebRexErpAPI.Business.Email.Dto;
using WebRexErpAPI.BusinessServices.Account.Dto;
using WebRexErpAPI.Data.UnitOfWork;
using WebRexErpAPI.Services.Account.Dto;
using WebRexErpAPI.Services.Common;
using WebRexErpAPI.Services.QuickBase;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using WebRexErpAPI.DataAccess.Models;
using WebRexErpAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace WebRexErpAPI.Services.Account.User
{
    public class UserServices:IUserService,IDisposable
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IQuickBaseService _quickBaseService;
        private readonly IEmailService _emailService;

        private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        public UserServices(
            JwtAuthenticationManager jwtAuthenticationManager , 
            IUnitOfWork unitOfWork,
            IQuickBaseService quickBaseService,
            IEmailService emailService
            )
        {
            _jwtAuthenticationManager = jwtAuthenticationManager;
            _unitOfWork = unitOfWork;
            _quickBaseService = quickBaseService;
            _emailService = emailService;
        }
        public string GetUserEmailClaime()
        {
            return _jwtAuthenticationManager.GetUserEmailClaime();
        }
        public async Task<UserVM?> Register(UserRegisterDto request)
        {
            try
            {
                var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);
                if (user != null && user.Id > 0)
                {
                    var data = new UserVM();
                    data.IsAlreadyRegistor = true;
                    return data;
                }
                else
                {
                    return await _jwtAuthenticationManager.Register(request);

                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }



        public async Task<UserVM?> Login(UserDto request)
        {
            return await _jwtAuthenticationManager.Login(request);
        }

        public async Task<UserVM> isUserExit(string email)
        {
            var userVm = new UserVM();
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(u => u.Email == email);
            if(user != null && user.Id > 0)
            {
                return await _jwtAuthenticationManager.UserToUserVMAndSetTokenAsync(userVm, user);
            }
            else
            {
                return null;
            }
            
        }

        public async Task<UserVM?> UpdateUserInfo(UserBindingDto request)
        {
            var user =   _unitOfWork.userRepository.FindAll(u => u.Id == request.Id).FirstOrDefault();
            user.BusinessName = request.BusinessName;
          //  user.Email = request.Email;
            user.FullName = request.FullName;

            CustomerQBDto customerQBDto = await _quickBaseService.FindCustomerQBBusiness(user.BusinessName);
           
                user.CustomerKeyQB = customerQBDto.QBId;
                user.CustomerNameQB = customerQBDto.CustomerName;
                user.CustomerQBId = customerQBDto.CustomerId;
            

            await _unitOfWork.userRepository.Update(user);
            await _unitOfWork.CompleteAsync();

            var userVm = new UserVM();
            return await _jwtAuthenticationManager.UserToUserVMAndSetTokenAsync(userVm,user);

        }

        public async Task<bool> UpdateFileUrlUser(UserBindingDto request)
        {
            var user = _unitOfWork.userRepository.FindAll(u => u.Id == request.Id).FirstOrDefault();
           if(user != null)
            {
                user.Examet = request.Examet;
                user.FileUrl = request.FileUrl;
                user.FileName = request.FileName;
                await _unitOfWork.userRepository.Update(user);
                await _unitOfWork.CompleteAsync();
                return true;
            }

            else
            {
                return false;
            }
           

        }

        public async Task<bool> ResetPasswordRequest(string email)
        {
            try
            {
                var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(u => u.Email == email);
                if (user == null)
                {
                    return false;
                }

                var otp = GenerateOTP();
                var otpExpiration = DateTime.UtcNow.AddMinutes(5);
                user.OtpHash = HashOTP(otp);
                user.OtpExpiration = otpExpiration;

                await _unitOfWork.userRepository.Update(user);
                await _unitOfWork.CompleteAsync();

                var _AppUrl = "https://kingsurplus.com/";

                EmailDto emailContent = new EmailDto();
                emailContent.ToAddress = email;
                emailContent.Subject = "OTP Verification";
                #region ebody

                emailContent.Body = @"
            <!DOCTYPE html PUBLIC ""-//W3C//DTD XHTML 1.0 Transitional//EN"" ""http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd"">
            <html xmlns=""http://www.w3.org/1999/xhtml"" xmlns:o=""urn:schemas-microsoft-com:office:office"">
             <head>
              <meta charset=""UTF-8"">
              <meta content=""width=device-width, initial-scale=1"" name=""viewport"">
              <meta name=""x-apple-disable-message-reformatting"">
              <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"">
              <meta content=""telephone=no"" name=""format-detection"">
              <title>Password Reset</title><!--[if (mso 16)]>
                <style type=""text/css"">
                a {text-decoration: none;}
                </style>
                <![endif]--><!--[if gte mso 9]><style>sup { font-size: 100% !important; }</style><![endif]--><!--[if gte mso 9]>
            <xml>
                <o:OfficeDocumentSettings>
                <o:AllowPNG></o:AllowPNG>
                <o:PixelsPerInch>96</o:PixelsPerInch>
                </o:OfficeDocumentSettings>
            </xml>
            <![endif]-->
              <style type=""text/css"">
            #outlook a {
	            padding:0;
            }
            .es-button {
	            mso-style-priority:100!important;
	            text-decoration:none!important;
            }
            a[x-apple-data-detectors] {
	            color:inherit!important;
	            text-decoration:none!important;
	            font-size:inherit!important;
	            font-family:inherit!important;
	            font-weight:inherit!important;
	            line-height:inherit!important;
            }
            .es-desk-hidden {
	            display:none;
	            float:left;
	            overflow:hidden;
	            width:0;
	            max-height:0;
	            line-height:0;
	            mso-hide:all;
            }
            @media only screen and (max-width:600px) {p, ul li, ol li, a { line-height:150%!important } h1, h2, h3, h1 a, h2 a, h3 a { line-height:120%!important } h1 { font-size:36px!important; text-align:left } h2 { font-size:26px!important; text-align:left } h3 { font-size:20px!important; text-align:left } .es-header-body h1 a, .es-content-body h1 a, .es-footer-body h1 a { font-size:36px!important; text-align:left } .es-header-body h2 a, .es-content-body h2 a, .es-footer-body h2 a { font-size:26px!important; text-align:left } .es-header-body h3 a, .es-content-body h3 a, .es-footer-body h3 a { font-size:20px!important; text-align:left } .es-menu td a { font-size:12px!important } .es-header-body p, .es-header-body ul li, .es-header-body ol li, .es-header-body a { font-size:14px!important } .es-content-body p, .es-content-body ul li, .es-content-body ol li, .es-content-body a { font-size:14px!important } .es-footer-body p, .es-footer-body ul li, .es-footer-body ol li, .es-footer-body a { font-size:14px!important } .es-infoblock p, .es-infoblock ul li, .es-infoblock ol li, .es-infoblock a { font-size:12px!important } *[class=""gmail-fix""] { display:none!important } .es-m-txt-c, .es-m-txt-c h1, .es-m-txt-c h2, .es-m-txt-c h3 { text-align:center!important } .es-m-txt-r, .es-m-txt-r h1, .es-m-txt-r h2, .es-m-txt-r h3 { text-align:right!important } .es-m-txt-l, .es-m-txt-l h1, .es-m-txt-l h2, .es-m-txt-l h3 { text-align:left!important } .es-m-txt-r img, .es-m-txt-c img, .es-m-txt-l img { display:inline!important } .es-button-border { display:inline-block!important } a.es-button, button.es-button { font-size:20px!important; display:inline-block!important } .es-adaptive table, .es-left, .es-right { width:100%!important } .es-content table, .es-header table, .es-footer table, .es-content, .es-footer, .es-header { width:100%!important; max-width:600px!important } .es-adapt-td { display:block!important; width:100%!important } .adapt-img { width:100%!important; height:auto!important } .es-m-p0 { padding:0!important } .es-m-p0r { padding-right:0!important } .es-m-p0l { padding-left:0!important } .es-m-p0t { padding-top:0!important } .es-m-p0b { padding-bottom:0!important } .es-m-p20b { padding-bottom:20px!important } .es-mobile-hidden, .es-hidden { display:none!important } tr.es-desk-hidden, td.es-desk-hidden, table.es-desk-hidden { width:auto!important; overflow:visible!important; float:none!important; max-height:inherit!important; line-height:inherit!important } tr.es-desk-hidden { display:table-row!important } table.es-desk-hidden { display:table!important } td.es-desk-menu-hidden { display:table-cell!important } .es-menu td { width:1%!important } table.es-table-not-adapt, .esd-block-html table { width:auto!important } table.es-social { display:inline-block!important } table.es-social td { display:inline-block!important } .es-m-p5 { padding:5px!important } .es-m-p5t { padding-top:5px!important } .es-m-p5b { padding-bottom:5px!important } .es-m-p5r { padding-right:5px!important } .es-m-p5l { padding-left:5px!important } .es-m-p10 { padding:10px!important } .es-m-p10t { padding-top:10px!important } .es-m-p10b { padding-bottom:10px!important } .es-m-p10r { padding-right:10px!important } .es-m-p10l { padding-left:10px!important } .es-m-p15 { padding:15px!important } .es-m-p15t { padding-top:15px!important } .es-m-p15b { padding-bottom:15px!important } .es-m-p15r { padding-right:15px!important } .es-m-p15l { padding-left:15px!important } .es-m-p20 { padding:20px!important } .es-m-p20t { padding-top:20px!important } .es-m-p20r { padding-right:20px!important } .es-m-p20l { padding-left:20px!important } .es-m-p25 { padding:25px!important } .es-m-p25t { padding-top:25px!important } .es-m-p25b { padding-bottom:25px!important } .es-m-p25r { padding-right:25px!important } .es-m-p25l { padding-left:25px!important } .es-m-p30 { padding:30px!important } .es-m-p30t { padding-top:30px!important } .es-m-p30b { padding-bottom:30px!important } .es-m-p30r { padding-right:30px!important } .es-m-p30l { padding-left:30px!important } .es-m-p35 { padding:35px!important } .es-m-p35t { padding-top:35px!important } .es-m-p35b { padding-bottom:35px!important } .es-m-p35r { padding-right:35px!important } .es-m-p35l { padding-left:35px!important } .es-m-p40 { padding:40px!important } .es-m-p40t { padding-top:40px!important } .es-m-p40b { padding-bottom:40px!important } .es-m-p40r { padding-right:40px!important } .es-m-p40l { padding-left:40px!important } .es-desk-hidden { display:table-row!important; width:auto!important; overflow:visible!important; max-height:inherit!important } }
            </style>
             </head>
             <body style=""width:100%;font-family:arial, 'helvetica neue', helvetica, sans-serif;-webkit-text-size-adjust:100%;-ms-text-size-adjust:100%;padding:0;Margin:0"">
              <div class=""es-wrapper-color"" style=""background-color:#FAFAFA""><!--[if gte mso 9]>
			            <v:background xmlns:v=""urn:schemas-microsoft-com:vml"" fill=""t"">
				            <v:fill type=""tile"" color=""#fafafa""></v:fill>
			            </v:background>
		            <![endif]-->
               <table class=""es-wrapper"" width=""100%"" cellspacing=""0"" cellpadding=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;padding:0;Margin:0;width:100%;height:100%;background-repeat:repeat;background-position:center top;background-color:#FAFAFA"">
                 <tr>
                  <td valign=""top"" style=""padding:0;Margin:0"">
                   <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                     <tr>
                      <td class=""es-info-area"" align=""center"" style=""padding:0;Margin:0"">
                       <table class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"" bgcolor=""#FFFFFF"">
                         <tr>
                          <td align=""left"" style=""padding:20px;Margin:0"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                    
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table>
                   <table cellpadding=""0"" cellspacing=""0"" class=""es-header"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0"">
                       <table bgcolor=""#ffffff"" class=""es-header-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"">
                         <tr>
                          <td align=""left"" style=""padding:20px;Margin:0"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td class=""es-m-p0r"" valign=""top"" align=""center"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0;padding-bottom:10px;font-size:0px"">
                                  <img src=""https://kingsurplus.com/assets/images/logo.webp"" alt=""Logo"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic;font-size:12px"" width=""340"" title=""Logo""></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table>
                   <table cellpadding=""0"" cellspacing=""0"" class=""es-content"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0"">
                       <table bgcolor=""#ffffff"" class=""es-content-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:#FFFFFF;width:600px"">
                         <tr>
                          <td align=""left"" style=""padding:0;Margin:0;padding-top:15px;padding-left:20px;padding-right:20px"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""center"" class=""es-m-p0r es-m-p0l es-m-txt-c"" style=""Margin:0;padding-top:15px;padding-bottom:15px;padding-left:40px;padding-right:40px""><h1 style=""Margin:0;line-height:37px;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-size:31px;font-style:normal;font-weight:bold;color:#333333"">Reset Your Password</h1></td>
                                 </tr>
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:40px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:24px;color:#333333;font-size:16px"">Please press the button below and enter the OTP code.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:23px;color:#333333;font-size:15px""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:33px;color:#333333;font-size:22px""><strong>OTP</strong> : <strong>" + otp + @"</strong></p></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                         <tr>
                          <td align=""left"" style=""padding:0;Margin:0;padding-bottom:20px;padding-left:20px;padding-right:20px"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:separate;border-spacing:0px;border-radius:5px"" role=""presentation"">
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0;padding-top:10px;padding-bottom:10px""><!--[if mso]><a href="""" target=""_blank"" hidden>
	            <v:roundrect xmlns:v=""urn:schemas-microsoft-com:vml"" xmlns:w=""urn:schemas-microsoft-com:office:word"" esdevVmlButton href="""" 
                            style=""height:44px; v-text-anchor:middle; width:329px"" arcsize=""14%"" stroke=""f""  fillcolor=""#0f174a"">
		            <w:anchorlock></w:anchorlock>
		            <center style='color:#ffffff; font-family:arial, ""helvetica neue"", helvetica, sans-serif; font-size:18px; font-weight:400; line-height:18px;  mso-text-raise:1px'>RESET YOUR PASSWORD</center>
	            </v:roundrect></a>
            <![endif]--><!--[if !mso]><!-- --><span class=""msohide es-button-border"" style=""border-style:solid;border-color:#2CB543;background:#0f174a;border-width:0px;display:inline-block;border-radius:6px;width:auto;mso-hide:all""><a href=" + _AppUrl + @"VerifyAccount/" + email + @" class=""es-button"" target=""_blank"" style=""mso-style-priority:100 !important;text-decoration:none;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;color:#FFFFFF;font-size:20px;padding:10px 30px 10px 30px;display:inline-block;background:#0f174a;border-radius:6px;font-family:arial, 'helvetica neue', helvetica, sans-serif;font-weight:normal;font-style:normal;line-height:24px;width:auto;text-align:center;mso-padding-alt:0;mso-border-alt:10px solid #0f174a;padding-left:30px;padding-right:30px"">RESET YOUR PASSWORD</a></span><!--<![endif]--></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                         <tr>
                          <td align=""left"" style=""padding:0;Margin:0;padding-top:20px;padding-left:20px;padding-right:20px"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""center"" valign=""top"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td align=""left"" style=""padding:0;Margin:0""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">If that doesn't work, copy and paste the following link in your browser:</p>
            <p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><a href=" + _AppUrl + @"VerifyAccount/" + email + @" target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#5C68E2;font-size:14px"">
                                   " + _AppUrl + @"/" + email + @"
            </a>&nbsp;</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">If you have any questions, just reply to this email—we're always happy to help out.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:21px;color:#333333;font-size:14px"">Cheers,<br>King Surplus Team</p></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table>
                   <table cellpadding=""0"" cellspacing=""0"" class=""es-footer"" align=""center"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;table-layout:fixed !important;width:100%;background-color:transparent;background-repeat:repeat;background-position:center top"">
                     <tr>
                      <td align=""center"" style=""padding:0;Margin:0"">
                       <table class=""es-footer-body"" align=""center"" cellpadding=""0"" cellspacing=""0"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px;background-color:transparent;width:600px"">
                         <tr>
                          <td align=""left"" style=""Margin:0;padding-top:20px;padding-bottom:20px;padding-left:20px;padding-right:20px"">
                           <table cellpadding=""0"" cellspacing=""0"" width=""100%"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                             <tr>
                              <td align=""left"" style=""padding:0;Margin:0;width:560px"">
                               <table cellpadding=""0"" cellspacing=""0"" width=""100%"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                 <tr>
                                  <td style=""padding:0;Margin:0"">
                                   <table cellpadding=""0"" cellspacing=""0"" width=""100%"" class=""es-menu"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                     <tr class=""links"">
                                      <td align=""center"" valign=""top"" width=""25%"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0""><a target=""_blank"" href=""https://kingsurplus.com/About"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#333333;font-size:12px"">About Us</a></td>
                                      <td align=""center"" valign=""top"" width=""25%"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0;border-left:1px solid #cccccc""><a target=""_blank"" href=""https://kingsurplus.com/PrivacyPolicy"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#333333;font-size:12px"">Privacy Policy</a></td>
                                      <td align=""center"" valign=""top"" width=""25%"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0;border-left:1px solid #cccccc""><a target=""_blank"" href=""https://kingsurplus.com/TermsOfService"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#333333;font-size:12px"">Terms of Services</a></td>
                                      <td align=""center"" valign=""top"" width=""25%"" style=""Margin:0;padding-left:5px;padding-right:5px;padding-top:5px;padding-bottom:5px;border:0;border-left:1px solid #cccccc""><a target=""_blank"" href=""https://kingsurplus.com"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:none;display:block;font-family:arial, 'helvetica neue', helvetica, sans-serif;color:#333333;font-size:12px"">Shop</a></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0;padding-top:15px;padding-bottom:15px;font-size:0"">
                                   <table cellpadding=""0"" cellspacing=""0"" class=""es-table-not-adapt es-social"" role=""presentation"" style=""mso-table-lspace:0pt;mso-table-rspace:0pt;border-collapse:collapse;border-spacing:0px"">
                                    <tr>
                                      <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:40px""><a target=""_blank"" href=""https://www.facebook.com/kingsurpluscompany"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""Facebook"" src=""https://qapdwr.stripocdn.email/content/assets/img/social-icons/logo-black/facebook-logo-black.png"" alt=""Fb"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                      <td align=""center"" valign=""top"" style=""padding:0;Margin:0;padding-right:40px""><a target=""_blank"" href=""https://www.youtube.com/channel/UCF8d0msOA2luUKhzA4Ap-JA"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px""><img title=""Youtube"" src=""https://qapdwr.stripocdn.email/content/assets/img/social-icons/logo-black/youtube-logo-black.png"" alt=""Yt"" width=""32"" style=""display:block;border:0;outline:none;text-decoration:none;-ms-interpolation-mode:bicubic""></a></td>
                                     </tr>
                                   </table></td>
                                 </tr>
                                 <tr>
                                  <td align=""center"" style=""padding:0;Margin:0;padding-bottom:35px""><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px"">© 2022 KingSurplus. All rights reserved. Trademarks and brands belong to their respective owners.</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px"">102 US-87, Comfort, TX 78013</p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px""><br></p><p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px"">This email was sent by KingSurplus If you don't want to receive this type of email in the future, please&nbsp;<a href=""https://kingsurplus.com/Profile"" target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px"">unsubscribe.</a></p>&nbsp;&nbsp;<p style=""Margin:0;-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;font-family:arial, 'helvetica neue', helvetica, sans-serif;line-height:18px;color:#333333;font-size:12px""><a href=""https://kingsurplus.com/TermsOfService"" target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px"">Terms of services</a>&nbsp;<a href=""https://kingsurplus.com/PrivacyPolicy"" target=""_blank"" style=""-webkit-text-size-adjust:none;-ms-text-size-adjust:none;mso-line-height-rule:exactly;text-decoration:underline;color:#333333;font-size:12px"">Privacy Policy</a></p></td>
                                 </tr>
                               </table></td>
                             </tr>
                           </table></td>
                         </tr>
                       </table></td>
                     </tr>
                   </table></td>
                 </tr>
               </table>
              </div>
             </body>
            </html>
";
                #endregion


                emailContent.Body = emailContent.Body.Replace("{otp}", otp);
                emailContent.Body = emailContent.Body.Replace("{email}", email);
                var IsSendEmail = await _emailService.EmailUsingAzureLogicApp(emailContent);

                return true;
            }
            catch (Exception )
            {
                throw ;
            }
        }

        public async Task<HttpResponseMessage> ConfirmResetPassword(PasswordResetModel input)
        {
            
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(u => u.Email == input.Email);
            if (user == null)
            {
                return   new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent("User not found")
                };
            }
           
            if (DateTime.UtcNow > user.OtpExpiration || !VerifyOTP(input.OTP, user.OtpHash))
            {
                return new HttpResponseMessage(HttpStatusCode.NotAcceptable)
                {
                    Content = new StringContent("Invalid OTP Code.")
                };
            }
            _jwtAuthenticationManager.CreatePasswordHash(input.NewPassword, out byte[] passwordHash, out byte[] passwordSalt);
                 user.Email = input.Email;
                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                await  _unitOfWork.userRepository.Update(user);
                await _unitOfWork.CompleteAsync();

            return new HttpResponseMessage(HttpStatusCode.OK)
            {

                Content = new StringContent("Reset password SuccessFully")
            };

        }

        private string GenerateOTP()
        {
            const int otpLength = 4;
            var random = new Random();
            var otp = new string(Enumerable.Repeat("0123456789", otpLength)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            return otp;
        }

        private string HashOTP(string otp)
        {
            using var sha256 = SHA256.Create();
            var otpBytes = Encoding.UTF8.GetBytes(otp.Trim());
            var hashBytes = sha256.ComputeHash(otpBytes);
            var hash = Convert.ToBase64String(hashBytes);
            return hash;
        }


   




        public async Task<bool> LoginAdmin(UserDto request)
        {
            var user = await _unitOfWork.userRepository.GetFirstOrDefaultAsync(u => u.Email == request.Email);

            
            if (user != null && VerifyPassword(request.Password, user.PasswordHash, user.PasswordSalt))
            {
                return true; 
            }
            else
            {
                return false; 
            }
        }

        private bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i])
                        return false; 
                }
            }
            return true; 
        }





        private bool VerifyOTP(string otp, string hashedOTP)
        {
            var hashedInputOTP = HashOTP(otp);
            var d = string.Equals(hashedOTP, hashedInputOTP);

            return d;
        }

        #region IDisposable Implementation

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _unitOfWork.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UserServices()
        {
            Dispose(false);
        }

        #endregion

    }
}
