using ApiRepository.Interfaces;
using DB.Models;
using Financial_Balance_API.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using static Financial_Balance_API.ApiSettings;

#nullable enable
namespace Financial_Balance_API.Controllers.Currencies
{
    [ApiController]
    public class CurrenciesController : Controller
    {
        private readonly ILogger _logger;
        private readonly IRepository<Currency> _repository;
        private readonly ApiSettings _apiSettings;
        //private readonly RequestDelegate _next;

        public CurrenciesController(ILogger<CurrenciesController> logger,
           IRepository<Currency> repository
           , IOptions<ApiSettings> options)
        {
            _logger = logger;
            _repository = repository;
            _apiSettings = options.Value;
        }

        // POST: Currencies Get All
        //[AdminKey]
        [HttpPost("api/currencies_get_all")]
        public async Task<IEnumerable<Currency>> GetAllCurrencies()
        {
            var TypeClient = GetCurrentKeyType();

            if (TypeClient == KeyType.None)
            {
                Response.StatusCode = 404;
                return null;
            }

            if (TypeClient == KeyType.Standard)
            {
                Response.StatusCode = 404;
                return null;
            }

            return await _repository.GetAll();
        }

        private KeyType GetCurrentKeyType()
        {
            try
            {
                return (KeyType)Request.HttpContext.Items["TypeClient"];
            }
            catch (Exception)
            {
                return KeyType.None;
            }
        }

        // POST: Add New Currency
        [HttpPost("api/CreateCurrency")]
        public async Task<ActionResult> CreateCurrency(string description,
            decimal factor,
            string isoCode,
            string symbol)
        {
            // Set Values
            var currency = new Currency();
            currency.Description = description;
            currency.Factor = factor;
            currency.IsoCode = isoCode;
            currency.Symbol = symbol;


            var result = await _repository.Insert(currency);

            return (result is null)
                ? NotFound()
                : Ok();
        }

        // POST: Add New Currency
        [AdminKey]
        [HttpPost("api/DeleteCurrency")]
        public async Task<ActionResult> DeleteCurrency(int id)
        {
            if (id <= 0)
            {
                return BadRequest();
            }

            var result = await _repository.Delete(id);

            return (result is null)
                ? BadRequest()
                : Ok();
        }

        //// GET: CurrenciesController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: CurrenciesController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: CurrenciesController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CurrenciesController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: CurrenciesController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: CurrenciesController/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: CurrenciesController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Delete(int id, IFormCollection collection)
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
