namespace ZupTeste.Infra.Settings
{
    public class AppSettings
    {
        public bool IsTestEnv { get; set; }
        
        public DatabaseConnection DatabaseConnection { get; set; }
        
        public JwtSettings JwtSettings { get; set; }
    }
}