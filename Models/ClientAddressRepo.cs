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

            if (_context.ClientAddressItem.Count() == 0)
                Add(new ClientAddressItem { Street = "Alamo 313", City = "Escobedo", State = "Nuevo Leon", Zip = "66058", Intersection1 = "Pino y Cedro", Active = true, DateStamp = DateTime.UtcNow });
        }

        public IEnumerable<ClientAddressItem> GetAll()
        {

            var allItems = _context.ClientAddressItem.ToList(); ;

            return allItems;
        }

        public LogMessageFromRepo Add(ClientAddressItem item)
        {
            try
            {
                _context.ClientAddressItem.Add(item);
                result = _context.SaveChanges();

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
                var entity = _context.ClientAddressItem.First(t => t.ID == id);
                _context.ClientAddressItem.Remove(entity);

                result = _context.SaveChanges();

                if (result != 1 && result > 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Oops.. more than one row were affected"; //esto en teoria nunca pasaria..
                }
                else if (result < 0)
                {
                    logMessage.Success = false;
                    logMessage.LogMessage = "Can't remove the selected address due to errors on the darkside"; //errores de lado de servidor de bd
                }

            }
            catch (Exception ex)
            {
                //error. TODO  create log de errores en db
                logMessage.Success = false;
                logMessage.LogMessage = "Something quite bad happend, ask Ruben for help.";
            }

            return logMessage;
        }

        public LogMessageFromRepo Update(ClientAddressItem item)
        {
            try
            {
                _context.ClientAddressItem.Update(item);
                result = _context.SaveChanges();

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
            return _context.ClientAddressItem.FirstOrDefault(t => t.ID == id);
        }
    }
}