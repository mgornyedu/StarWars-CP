using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Api.Controllers
{
    public class AsyncController : ControllerBase
    {
        /// <summary>
        /// Funkcja obsługująca asynchroniczne pobieranie
        /// </summary>
        /// <typeparam name="T">Typ zwracanych danych</typeparam>
        /// <param name="task">Przekazywane zadanie, które ma zostać obsłużone przez funckję</param>
        /// <returns>Zwraca asynchronicznie resultat akcji</returns>
        public async Task<IActionResult> AsyncGet<T>(Task<T> task)
        {
            try
            {
                var value = await task;
                if (value == null)
                    return NotFound();
                return Ok(value);
            }
            catch
            {
                return BadRequest();
            }
        }
        /// <summary>
        /// Funkcja obsługująca asynchroniczne zapis.
        /// </summary>
        /// <param name="task">Przekazywane zadanie, które ma zostać obsłużone przez funckję</param>
        /// <returns>Zwraca asynchronicznie resultat akcji</returns>
        public async Task<IActionResult> AsyncSave(Task<int> task)
        {
            try
            {
                var result = await task;
                if (result == 0)
                    return NotFound();
                return Ok(result);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
