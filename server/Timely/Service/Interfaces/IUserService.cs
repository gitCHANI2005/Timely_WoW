using Service.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface IUserService
    {
        UserDto ValidateUser(string name, string password);
        bool IsEmailTaken(string email);
        string RegisterDeliver( DeliverDto deliver);
        string RegisterCustomer(CustomerDto customer);
        string RegisterOwner(OwnerDto owner);

    }
}
