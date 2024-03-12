using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.Tecnico.Mottu.Matheus.Willock.Application.DTOs.UserAdmin
{
    public record InputUserAdminToUpdateDto(string Name, string Email, string Password)
    {
    }
}
