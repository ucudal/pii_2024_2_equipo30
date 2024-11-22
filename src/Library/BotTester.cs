namespace Library
{
    public class BotTester
    {
        public string GetGreeting(string userName)
        {
            return $"Hola, {userName}. ¡Bienvenido al programa principal!";
        }

        public string PerformCalculation(int a, int b)
        {
            return $"El resultado de sumar {a} y {b} es {a + b}.";
        }
    }
}