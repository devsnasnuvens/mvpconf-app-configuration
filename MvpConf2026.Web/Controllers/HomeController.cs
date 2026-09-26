using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement;
using MvpConf2026.Web.Models;

namespace MvpConf2026.Web.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _configuration;
    // private readonly IFeatureManager _featureManager;

    public HomeController(IConfiguration configuration) //, IFeatureManager featureManager)
    {
        _configuration = configuration;
        // _featureManager = featureManager;
    }

    public async Task<IActionResult> Index()
    {
        ViewData["Mensagem"] = _configuration["MVPConf:Mensagem"];

        // if (await _featureManager.IsEnabledAsync("SuperFeature"))
        // {
        //     ViewData["SuperFeatureMensagem"] = "A nova super feature foi habilitada!";
        // }

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
