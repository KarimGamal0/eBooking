namespace eBooking.Dtos
{
    public class ClientRegisterDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PassWord { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
