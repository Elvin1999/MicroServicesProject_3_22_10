using Contact.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact.API.Infrastructure
{
    public interface IContactService
    {
        ContactModel GetContactById(int id);
        List<ContactModel> GetAll();
        ContactModel? Add(ContactModel model);
        ContactModel? Update(int id,ContactModel model);
        bool Delete(int id);
    }
}
