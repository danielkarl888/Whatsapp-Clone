using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WAppBIU_Server.Models;

namespace Services
{
    public interface IRankService
    {
        public List<Rank> GetAllRanks();

        public Rank GetRankById(int id);

        public List<Rank> QuerySearch(string query);
        public void AddRank(Rank r);
        public void DeleteRank(Rank r);


    }


}
