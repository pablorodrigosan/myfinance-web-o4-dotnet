using Microsoft.AspNetCore.Mvc;
using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Controllers
{
    [Route("[controller]")]
    public class PlanoContaController : Controller
    {
        private readonly ILogger<PlanoContaController> _logger;
        private readonly MyFinanceDbContext _myFinanceDbContext;

        public PlanoContaController(
            ILogger<PlanoContaController> logger,
            MyFinanceDbContext myFinanceDbContext)
        {
            _logger = logger;
            _myFinanceDbContext = myFinanceDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listaPlanoContas = _myFinanceDbContext.PlanoConta;
            var listaPlanoContaModel = new List<PlanoContaModel>();

            foreach (var item in listaPlanoContas){
                var planoContaModel = new PlanoContaModel()
                {
                    Id = item.Id,
                    Descricao = item.Descricao,
                    Tipo = item.Tipo
                };

                listaPlanoContaModel.Add(planoContaModel);
            }

            ViewBag.ListaPlanoConta = listaPlanoContaModel;

            return View();
        }

        [HttpGet]
        [Route("Cadastro")]
        [Route("Cadastro/{id}")]
        public IActionResult Cadastro(int? id)
        {
            return View();
        }

        [HttpPost]
        [Route("Cadastro")]
        [Route("Cadastro/{input}")]
        public IActionResult Cadastro(PlanoContaModel input)
        {
            var planoConta = new PlanoConta(){
                Id = input.Id,
                Descricao = input.Descricao,
                Tipo = input.Tipo
            };

            _myFinanceDbContext.PlanoConta.Add(planoConta);
            _myFinanceDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}