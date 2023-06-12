using GameFinale.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GameFinale.Controllers
{
    public class GestioneController : Controller
    {
        private readonly EcommerceContext _context;

        public GestioneController(EcommerceContext context)
        {
            _context = context;
        }
        public IActionResult ListaProdotti()
        {
            var query = _context.Prodottos.ToList();

            return View(query);
        }

        

        public IActionResult NuovoProdotto()
        {
            ViewBag.NuovoId = _context.Prodottos.Max(c => c.IdProdotto) + 1;
            return View();
        }

        [HttpPost]
        public IActionResult NuovoProdotto(Prodotto prodotto)
        {
            _context.Prodottos.Add(prodotto);
            _context.SaveChanges();
            return RedirectToAction("ListaProdotti");
        }

        public IActionResult EliminaProdotto(int id)
        {
            var prodotto = _context.Prodottos.Find(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            return View(prodotto);
        }
        [HttpPost]
        [ActionName("EliminaProdotto")]
        public IActionResult EliminaProdottoConferma(int id)
        {
            var prodotto = _context.Prodottos.Find(id);
            if (prodotto == null)
            {
                return NotFound();
            }
            _context.Prodottos.Remove(prodotto);
            _context.SaveChanges();
            return RedirectToAction("ListaProdotti");
        }

        public IActionResult ListaUtenti()
        {
            var query = _context.Utentes.ToList();

            return View(query);
        }

        public IActionResult NuovoUtente()
        {
            ViewBag.NuovoId = _context.Utentes.Max(c => c.IdUtente) + 1;
            return View();
        }
        [HttpPost]
        public IActionResult NuovoUtente(Utente utente)
        {
            _context.Utentes.Add(utente);
            _context.SaveChanges();
            return RedirectToAction("ListaUtenti");
        }

        public IActionResult EliminaUtente(int id)
        {
            var utente = _context.Utentes.Find(id);
            if (utente == null)
            {
                return NotFound();
            }
            return View(utente);
        }
        [HttpPost]
        [ActionName("EliminaUtente")]
        public IActionResult EliminaUtenteConferma(int id)
        {
            var utente = _context.Utentes.Find(id);
            if (utente == null)
            {
                return NotFound();
            }
            _context.Utentes.Remove(utente);
            _context.SaveChanges();
            return RedirectToAction("ListaUtenti");
        }

        public IActionResult Carrello()
        {
            var query = _context.Carrellos.ToList();

            return View(query);
        }

        

        public IActionResult AggiungiAlCarrello(int id_prodotto)
        {
            var carrello = new Carrello();
            carrello.IdProdotto = id_prodotto;
            if (carrello == null)
            {
                return NotFound();
            }
            return View(carrello);
        }
        [HttpPost]
        public IActionResult AggiungiAlCarrello(int id_prodotto, int id_carrello, int quantita, string prezzo)
        {
            var carrello = _context.Carrellos.Find(id_prodotto);
            if (carrello == null)
            {
                return NotFound();
            }
            
            carrello.IdCarrello = id_carrello;
            carrello.IdProdotto = id_prodotto;
            carrello.QuantitaTotale = quantita;
            carrello.Prezzo = prezzo;
            _context.Carrellos.Add(carrello);
            _context.SaveChanges();
            return RedirectToAction("Carrello");
        }

    }
}
