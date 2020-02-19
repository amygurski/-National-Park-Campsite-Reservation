using Capstone.Models;
using System.Collections.Generic;

namespace Capstone.DAL
{
    public interface ISiteDAO
    {
        List<Site> GetSites();
    }
}