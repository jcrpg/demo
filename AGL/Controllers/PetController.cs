using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AGL.Infrastructure;
using AGL.Models;
using AGL.Services;

namespace AGL.Controllers
{
    public class PetController : ApiController, ICustomerController
    {

        public async Task<List<RootObject>> GetPet(CancellationToken cToken)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (!cToken.IsCancellationRequested)
            {
                //instead of debug I can use log4net to either save as a log file or integrate with SumoLogic web service
                var result = WebClientManager.GetItemList().Result;
                Debug.WriteLine("elapsed ms:{0}", sw.ElapsedMilliseconds);

                return result;
            }
            else
            {
                return null;
            }
        }
        public async Task<IEnumerable<CategoryViewModel>> GetPet(CancellationToken cToken, string category)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (!cToken.IsCancellationRequested)
            {
                //log4net
                var result = WebClientManager.GetItemList().Result;
                Debug.WriteLine("elapsed ms:{0}", sw.ElapsedMilliseconds);
                foreach (var owner in result.ToList())
                {
                    List<Pet> removeList = new List<Pet>();

                    if (owner.pets != null)
                    {
                        for (int i = 0; i < owner.pets.Count; i++)
                        {
                            if (!owner.pets[i].type.Equals(category, StringComparison.OrdinalIgnoreCase))
                            {
                                owner.pets.RemoveAt(i);
                                i--;
                            }

                        }

                    }


                }

                var grouping = (from p in (from p in result
                                           where p.pets != null
                                           where
                                               (from t in p.pets
                                                where t.type.ToLower() == category
                                                select t
                                                   ).Any()
                                           select p
                    )
                    group p.pets by p.gender into g
                    select new CategoryViewModel()
                    {
                        gender = g.Key,
                        pets = g.ToList()

                    });
                return grouping;
            }
            //basic validation if result is null
            throw new HttpResponseException(HttpStatusCode.NoContent);
        }
    }
}
