using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAppBIU_Server.Models;

namespace Services
{
    public class RankService : IRankService
    {
        public static List<Rank> ranks = new List<Rank> { new Rank { Id = 1 , Name = "daniel", Number = 3, Text ="good!", Time=DateTime.Now},
                                                          new Rank { Id = 1 , Name = "daniel", Number = 3, Text ="good!", Time=DateTime.UtcNow}};




        public List<Rank> GetAllRanks()
        {
            return ranks;
        }

        public Rank GetRankById(int id)
        {
            return ranks.Find(x=>x.Id==id);
        }

        public List<Rank> QuerySearch(string query)
        {
            return ranks.FindAll(x=>x.Name.Contains(query) || x.Text.Contains(query));
        }

        public void AddRank(Rank r)
        {
            ranks.Add(r);
        }
        public void DeleteRank(Rank r)
        {
            ranks.Remove(r);
        }



    }


}
