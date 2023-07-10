using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myfinance_web_netcore.Domain;
using myfinance_web_netcore.Models;

namespace myfinance_web_netcore.Controllers
{
    [Route("[controller]")]
    public class TransacaoController : Controller
    {
        private readonly ILogger<TransacaoController> _logger;
        private readonly MyFinanceDbContext _myFinanceDbContext;

        public TransacaoController(
            ILogger<TransacaoController> logger,
            MyFinanceDbContext myFinanceDbContext)
        {
            _logger = logger;
            _myFinanceDbContext = myFinanceDbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var listaTransacao = _myFinanceDbContext.Transacao.Include(x => x.PlanoConta);
            var listaTransacaoModel = new List<TransacaoModel>();

            foreach (var item in listaTransacao)
            {
                var PlanoContaModel = new PlanoContaModel()
                {
                    Id = item.PlanoConta.Id,
                    Descricao = item.PlanoConta.Descricao,
                    Tipo = item.PlanoConta.Tipo
                };
                
                var transacaoModel = new TransacaoModel(){
                    Id = item.Id,
                    Historico = item.Historico,
                    Data = item.Data,
                    Valor = item.Valor,
                    PlanoContaId = item.PlanoContaId,
                    ItemPlanoConta = PlanoContaModel
                };

                listaTransacaoModel.Add(transacaoModel);
            }

            ViewBag.Transacoes = listaTransacaoModel;

            return View();
        }

        [HttpGet]
        [Route("Cadastro")]
        [Route("Cadastro/{id}")]
        public IActionResult Cadastro(int? id)
        {
            var itensPlanoConta = _myFinanceDbContext.PlanoConta;

            var transacaoModel = new TransacaoModel();

            List<SelectListItem> itensPlano = new();
            foreach(var item in itensPlanoConta)
            {
            itensPlano.Add(new SelectListItem() { Text = item.Descricao, Value = item.Id.ToString() });
            }

            transacaoModel.PlanoContas = itensPlano;

            return View(transacaoModel);
        }

        [HttpPost]
        [Route("Cadastro")]
        [Route("Cadastro/{id}")]
        public IActionResult Cadastro(PlanoContaModel input)
        {
            var planoConta = new PlanoConta()
            {
                Id = input.Id,
                Descricao = input.Descricao,
                Tipo = input.Tipo
            };

            if (planoConta.Id == null)
                _myFinanceDbContext.PlanoConta.Add(planoConta);
            else
            {
                _myFinanceDbContext.PlanoConta.Attach(planoConta);
                _myFinanceDbContext.Entry(planoConta).State = EntityState.Modified;
            }

            _myFinanceDbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        [HttpGet]
        [Route("Excluir/{id}")]
        public IActionResult Excluir(int id)
        {
            var planoConta = new PlanoConta() { Id = id };
            _myFinanceDbContext.PlanoConta.Remove(planoConta);
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