namespace aspnetcoremvc_adonet.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string usuario { get; set; }
        public string senha { get; set; }
        public string tipo { get; set; }
        public string confSenha { get; set; }

        public string nomeCli { get; set; }

        public string telCli { get; set; }
        public string EmailCli { get; set; }
    }
}
