﻿using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using WebDientesitos.Models;
using WebDientesitos.Service.Interface;

namespace WebDientesitos.Service.Repository
{
    public class UtilityRepository : IUtility
    {
        public void enviarCorreo(string destinatario, string asunto, string cuerpo)
        {
            var cliente = new SmtpClient("smtp.gmail.com", 587)
            {
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("dientesitosweb@gmail.com", "yfqyatlzgvclibrw")
            };

            var email = new MailMessage("dientesitosweb@gmail.com", destinatario);
            email.Subject = asunto;
            email.Body = cuerpo;
            email.IsBodyHtml = false;
            cliente.Send(email);
        }

        public string convertirSha256(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = sha256.ComputeHash(inputBytes);

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    builder.Append(hashBytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }

        public string generarClaveTemporal()
        {
            String Characters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[7];
                rng.GetBytes(bytes);

                return new String(bytes.Select(b => Characters[b % Characters.Length]).ToArray());
            }
        }

        public string mensajeCitaReservada(Paciente paciente)
        {
            throw new NotImplementedException();
        }

        public string mensajeCLaveCreada(Paciente paciente)
        {
            return @"Estimado/a " + paciente.Nombre + @" " + paciente.ApellidoPaterno + @",

                    Esperamos que este correo le encuentre bien. Como parte del equipo de Dientesitos, su consultorio odontológico de confianza, nos complace brindarle acceso a su cuenta en nuestra plataforma web.

                    Hemos generado una clave temporal para su cuenta, la cual le permitirá acceder a nuestros servicios en línea. Le recordamos que es de vital importancia que cambie esta contraseña temporal cuando inicie sesión por primera vez. Esto ayudará a garantizar la seguridad de su cuenta y proteger su información personal.

                    A continuación, le proporcionamos los detalles necesarios para iniciar sesión en su cuenta:

                    Opción de inicio de sesión: Paciente
                    DNI: [Su número de DNI]
                    Contraseña temporal: " + paciente.Constrasena + @"

                    Por favor, siga los pasos a continuación para iniciar sesión por primera vez:

                    1. Acceda a nuestro sitio web en [URL del sitio web].
                    2. Seleccione la opción ""Paciente"" en la página de inicio de sesión.
                    3. Ingrese su número de DNI y la contraseña temporal proporcionada arriba.
                    4. Una vez iniciada la sesión, se le pedirá que cambie su contraseña temporal. Asegúrese de elegir una contraseña segura y que sea fácil de recordar.

                    Si tiene alguna pregunta o necesita asistencia adicional, no dude en comunicarse con nuestro equipo de soporte al cliente. Estamos aquí para ayudarlo en todo momento.

                    Agradecemos su confianza en Dientesitos y esperamos poder brindarle una experiencia odontológica excepcional en nuestra plataforma web.

                    ¡Saludos cordiales!

                    Equipo de Dientesitos";
        }
    }
}
