using FluentValidation;

namespace CallLog
{
    class Validation : AbstractValidator<string>
    {

        public bool EmailValidation(string email)
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }




    }
}
