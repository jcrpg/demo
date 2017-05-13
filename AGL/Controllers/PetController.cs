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
        private static string TargetUrl = "";

        public async Task<List<RootObject>> GetPet(CancellationToken cToken)
        {
            Stopwatch sw = Stopwatch.StartNew();

            if (!cToken.IsCancellationRequested)
            {
                //log4net
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

                //var newList = (from p in result
                //               where p.pets!=null
                //    where
                //        (from t in p.pets
                //            where t.type == "Cat"
                //            select t
                //            ).Any()
                //    select p
                //    );
                //.ToList().SelectMany(o=>o.pets.SelectMany(g=>g.type=="Cat"));
                //result = result.Where(r => r.pets != null).ToList();


                foreach (var owner in result.ToList())
                {
                    List<Pet> removeList = new List<Pet>();

                    //foreach (var pet in owner.pets)
                    //{
                    //        //if (!pet.type.Equals(category, StringComparison.OrdinalIgnoreCase))
                    //        //{
                    //        //removeList.Add(pet);
                    //        //}

                    //}
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


                //foreach (var gender in newList2)
                //{

                //    foreach (var pets in gender.pets)
                //    {
                //        foreach (var pet in pets)
                //        {
                //            if (pet.type.Equals(category, StringComparison.OrdinalIgnoreCase))
                //            {
                //                newList.pets.Add(pet);
                //            }

                //        }

                //        //for (int i = 0; i < pets.Count; i++)
                //        //{
                //        //    if (!pets[0].type.Equals(category, StringComparison.OrdinalIgnoreCase))
                //        //    {
                //        //        pets.RemoveAt(i);
                //        //        i--;
                //        //    }

                //        //}

                //    }
                //}

                return grouping;
            }
            //basic validation if result is null
            throw new HttpResponseException(HttpStatusCode.NoContent);
        }
    }
}
