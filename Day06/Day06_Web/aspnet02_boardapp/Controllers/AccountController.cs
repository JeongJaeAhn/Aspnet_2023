﻿using aspnet02_boardapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace aspnet02_boardapp.Controllers
{
    // 사용자 회원가입, 로그인, 로그아웃
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            // Null 검사추가 체크
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        // https://localhost:7155/Account/Register
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        // 비동기가 아니면 return값은 IActionResult
        // 비동기가 되면 Task<IActionResult>
        public async Task<IActionResult> Register(RegisterModel model)
        {
            ModelState.Remove("PhoneNumber"); // PhoneNumber는 입력값 검증에서 제거

            if (ModelState.IsValid)
            {
                // ASP.NET user - aspnetusers 테이블에 데이터넣기 위해서
                // 매핑되는 인스턴스를 생성
                var user = new IdentityUser 
                {
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber // 핸드폰 번호 추가
                };

                // aspnetusers 테이블에 사용자 데이터를 대입
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // 회원가입을 성공했으면 로그인한 뒤 localhost:7155/Home/Index 로 가라
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    TempData["succeed"] = "회원가입을 축하합니다."; // 성공메세지
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model); // 회원가입을 실패하면 그화면 그대로 유지
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login (LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // 파라미터 순서 : 아이디, 패스워드, IsPersistent = RememberMe, 실패할때 계정 잠그기
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    TempData["succeed"] = "로그인했습니다."; // 성공메세지
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "로그인 실패!");
            }

            return View(model); // 입력검증이 실패하면 화면에 그대로 대기
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            TempData["succeed"] = "로그아웃했습니다."; // 성공메세지
            return RedirectToAction("Index", "Home");
        }
    }
}