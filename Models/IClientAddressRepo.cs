using System.Collections.Generic;

namespace RubenAddressClientWepApp.Models
{
    public interface IClientAddressRepo
    {
        IEnumerable<ClientAddressItem> GetAll();
        LogMessageFromRepo Add(ClientAddressItem item);

        ClientAddressItem Find(int id);
        
        LogMessageFromRepo Update(ClientAddressItem item);

        LogMessageFromRepo Remove(int id);
         
    }
}