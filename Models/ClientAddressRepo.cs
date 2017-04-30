using System;
using System.Collections.Generic;
using System.Linq;

namespace RubenAddressClientWepApp.Models
{
    public class ClientAddressRepo : IClientAddressRepo
    {
        private ClientAddressContext _context;
        private int result = 0;
        public LogMessageFromRepo logMessage = new LogMessageFromRepo() { Success = true, LogMessage = String.Empty };

        public ClientAddressRepo(ClientAddressContext context)
        {
            _context = context;
        }

        public IEnumerable<ClientAddressItem> GetAll()
        {
            List<ClientAddressItem> allItems = new List<ClientAddressItem>();

            var itemsResult = _context.RubenClientAddressSEL(iD: null).ToList(); ;

            foreach (var item in itemsResult)
            {
                ClientAddressItem clientAddress = new ClientAddressItem()
                {
                    ID = item.ID,
                    Street = item.Street,
                    City = item.City,
                    State = item.State,
                    Zip = item.Zip,
                    Intersection1 = item.Intersection1,
                    Active = item.Active,
                    DateStamp = item.DateStamp

                };

                allItems.Add(clientAddress);
            }

            return allItems;
        }

        public LogMessageFromRepo Add(ClientAddressItem item)
        {
            try
            {
                result = _context.RubenClientAddressSI(item.Street, item.City, item.State, item.Zip, item.Intersection1);

                if (result <= 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Can't create the new address due to errors on the darkside";
                }
            }
            catch (Exception ex) // TODO  create log de errores en db
            {

                logMessage.Success = false;
                logMessage.LogMessage = "Something quite bad happend, ask Ruben for help.";
            }

            return logMessage;
        }

        public LogMessageFromRepo Remove(int id)
        {

            try
            {
                result = _context.RubenClientAddressSD(id);

                if (result != 1 && result > 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Oops.. more than one row were affected";
                }
                else if (result < 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Can't remove the selected address due to errors on the darkside";
                }

            }
            catch (Exception ex) // TODO  create log de errores en db
            {

                logMessage.Success = false;
                logMessage.LogMessage = "Something quite bad happend, ask Ruben for help.";
            }


            return logMessage;
        }

        public LogMessageFromRepo Update(ClientAddressItem item)
        {
            try
            {
                result = _context.RubenClientAddressSU(item.ID, item.Street, item.City, item.State, item.Zip, item.Intersection1);

                if (result != 1 && result > 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Oops.. more than one row were affected";
                }
                else if (result < 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Can't update the selected address due to errors on the darkside";
                }
            }
            catch (Exception ex) // TODO  create log de errores en db
            {

                logMessage.Success = false;
                logMessage.LogMessage = "Something quite bad happend, ask Ruben for help.";
            }

            return logMessage;
        }

        public ClientAddressItem Find(int id)
        {
            ClientAddressItem item = new ClientAddressItem();

            try
            {
                var itemResult = _context.RubenClientAddressSEL(iD: id).ToList().SingleOrDefault();

                item.Active = itemResult.Active;
                item.City = itemResult.City;
                item.DateStamp = itemResult.DateStamp;
                item.ID = itemResult.ID;
                item.Intersection1 = itemResult.Intersection1;
                item.State = itemResult.State;
                item.Street = itemResult.Street;
                item.Zip = itemResult.Zip;
            }
            catch (Exception ex) // TODO log exception
            {
                return new ClientAddressItem();
                throw;
            }


            return item;
        }
    }
}