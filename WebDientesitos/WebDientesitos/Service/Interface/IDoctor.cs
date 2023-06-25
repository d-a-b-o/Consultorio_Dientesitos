using WebDientesitos.Models;

namespace WebDientesitos.Service.Interface
{
    public interface IDoctor
    {
        Doctor          getDoctor(HttpContext httpContext);
        void            editDoctor(Doctor docEdit);
        DatosCitaDoctor getDatosCita(int idDoctor);
    }
}
