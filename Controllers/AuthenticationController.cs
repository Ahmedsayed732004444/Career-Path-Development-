using Career_Path.Services;
using Microsoft.AspNetCore.Mvc;

namespace Career_Path.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController(IGitHubAuthService gitHubAuthService) : ControllerBase
    {
        private readonly IGitHubAuthService _gitHubAuthService = gitHubAuthService;

        [HttpGet("github/login")]
        public IActionResult GitHubLogin([FromQuery] string returnUrl = "")
        {
            var properties = _gitHubAuthService.ConfigureGitHubLogin(returnUrl);
            return Challenge(properties, "GitHub");
        }

        [HttpGet("github/callback")]
        public async Task<IActionResult> GitHubLoginCallback([FromQuery] string returnUrl = "", [FromQuery] string remoteError = "")
        {
            var result = await _gitHubAuthService.HandleGitHubCallbackAsync(remoteError);

            if (result.IsFailure)
                return result.ToProblem();

            // إذا كنت تستخدم SPA (React/Angular/Vue)
            return Ok(result.Value);

            // إذا كنت تريد إعادة التوجيه لصفحة معينة (غير موصى به للـ tokens)
            // if (!string.IsNullOrEmpty(returnUrl))
            // {
            //     return Redirect($"{returnUrl}#access_token={result.Value.Token}");
            // }
        }
    }
}



//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Identity;
//using System.Security.Claims;

//namespace Career_Path.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class AuthenticationController : ControllerBase
//    {
//        private readonly SignInManager<ApplicationUser> _signInManager;
//        private readonly UserManager<ApplicationUser> _userManager;
//        private readonly IJwtProvider _jwtProvider;

//        public AuthenticationController(
//            SignInManager<ApplicationUser> signInManager,
//            UserManager<ApplicationUser> userManager,
//            IJwtProvider jwtProvider)
//        {
//            _signInManager = signInManager;
//            _userManager = userManager;
//            _jwtProvider = jwtProvider;
//        }

//        [HttpGet("github/login")]
//        public IActionResult GitHubLogin([FromQuery] string returnUrl = "")
//        {
//            var redirectUrl = "http://localhost:5250/api/authentication/github/callback";
//            var properties = _signInManager.ConfigureExternalAuthenticationProperties("GitHub", redirectUrl);
//            return new ChallengeResult("GitHub", properties);
//        }


//        [HttpGet("github/callback")]
//        public async Task<IActionResult> GitHubLoginCallback([FromQuery] string returnUrl = "", [FromQuery] string remoteError = "")
//        {
//            // 1️⃣ التحقق إذا في خطأ رجع من GitHub أثناء المصادقة
//            if (!string.IsNullOrEmpty(remoteError))
//            {
//                return BadRequest(new
//                {
//                    success = false,
//                    error = $"Error from GitHub: {remoteError}" // إرسال رسالة الخطأ للمستخدم
//                });
//            }

//            // 2️⃣ جلب معلومات تسجيل الدخول الخارجية من GitHub
//            var info = await _signInManager.GetExternalLoginInfoAsync();
//            if (info == null)
//            {
//                return BadRequest(new
//                {
//                    success = false,
//                    error = "Unable to load external login information from GitHub" // إذا مفيش معلومات، ارجع خطأ
//                });
//            }

//            // 3️⃣ محاولة تسجيل الدخول مباشرة إذا الحساب موجود مسبقًا
//            var signInResult = await _signInManager.ExternalLoginSignInAsync(
//                info.LoginProvider,    // GitHub
//                info.ProviderKey,      // المفتاح الفريد للحساب
//                isPersistent: false,   // عدم الاحتفاظ بالدخول بعد إغلاق المتصفح
//                bypassTwoFactor: true  // تجاوز التحقق بخطوتين لو موجود
//            );

//            ApplicationUser user; // متغير لتخزين بيانات المستخدم

//            if (signInResult.Succeeded)
//            {
//                // 4️⃣ إذا تسجيل الدخول نجح، جلب بيانات المستخدم من قاعدة البيانات
//                user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
//            }
//            else
//            {
//                // 5️⃣ إذا الحساب مش موجود مسبقًا، جلب البريد الإلكتروني من بيانات GitHub
//                var userEmail = info.Principal.FindFirstValue(ClaimTypes.Email);

//                if (string.IsNullOrEmpty(userEmail))
//                {
//                    return BadRequest(new
//                    {
//                        success = false,
//                        error = "Email not provided by GitHub" // الخطأ لو GitHub ما رجعش الإيميل
//                    });
//                }

//                // 6️⃣ تحقق إذا المستخدم موجود بالبريد الإلكتروني في قاعدة البيانات
//                user = await _userManager.FindByEmailAsync(userEmail);

//                if (user == null)
//                {
//                    // 7️⃣ إنشاء مستخدم جديد إذا ماكانش موجود
//                    user = new ApplicationUser()
//                    {
//                        UserName = userEmail,
//                        Email = userEmail,
//                        EmailConfirmed = true // تأكيد البريد تلقائيًا
//                    };

//                    var createResult = await _userManager.CreateAsync(user); // حفظ المستخدم في قاعدة البيانات

//                    if (!createResult.Succeeded)
//                    {
//                        return BadRequest(new
//                        {
//                            success = false,
//                            error = "Failed to create user",
//                            details = createResult.Errors.Select(e => e.Description) // إرسال تفاصيل الأخطاء
//                        });
//                    }

//                    // 8️⃣ إضافة الدور "User" للمستخدم الجديد
//                    await _userManager.AddToRoleAsync(user, "User");
//                }

//                // 9️⃣ ربط حساب GitHub بالمستخدم الجديد أو الموجود
//                var addLoginResult = await _userManager.AddLoginAsync(user, info);

//                if (!addLoginResult.Succeeded)
//                {
//                    return BadRequest(new
//                    {
//                        success = false,
//                        error = "Failed to link GitHub account",
//                        details = addLoginResult.Errors.Select(e => e.Description)
//                    });
//                }

//                // 10️⃣ تسجيل الدخول للمستخدم بعد إنشاء الحساب أو ربطه
//                await _signInManager.SignInAsync(user, isPersistent: false);
//            }

//            // 11️⃣ توليد JWT Token للمستخدم بعد تسجيل الدخول
//            var roles = await _userManager.GetRolesAsync(user); // جلب أدوار المستخدم
//            var permissions = new List<string>(); // يمكن إضافة الصلاحيات لو موجودة
//            var (tokenString, expiresIn) = _jwtProvider.GenerateToken(user, roles, permissions); // توليد التوكن

//            // 12️⃣ إرجاع الرد النهائي للعميل مع بيانات المستخدم والتوكن
//            return Ok(new
//            {
//                success = true,
//                message = signInResult.Succeeded ? "Login successful" : "Account created and login successful",
//                token = tokenString,
//                user = new
//                {
//                    id = user.Id,
//                    email = user.Email,
//                    userName = user.UserName
//                }
//            });
//        }

//    }
//}
