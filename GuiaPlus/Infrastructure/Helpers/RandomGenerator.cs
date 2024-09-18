namespace GuiaPlus.Infrastructure.Helpers
{
    public static class RandomGenerator
    {
        private static readonly char[] chars =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789".ToCharArray();

        public static string GenerateGuiaNumber()
        {
            // Pega os primeiros 10 dígitos do timestamp atual (formato yyyyMMddHHmmss)
            string timestamp = DateTime.UtcNow.ToString("yyyyMMddHHmm").Substring(0, 10);

            // Completa com numeros aleatórios
            Random random = new Random();
            int randomPart = random.Next(10000, 99999); // 5 dígitos

            // Combina o timestamp com o número aleatório, total de 15 digítos
            return $"{timestamp}{randomPart}";
        }
    }
}
