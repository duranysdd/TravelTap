public static class FractionalDamageSystem
{
    private static float buffer = 0f;

    public static void AddDamage(float amount)
    {
        buffer += amount;

        if (buffer >= 1f)
        {
            int aplicar = (int)buffer;
            buffer -= aplicar;

            GameManager.instance.TomarDaño(aplicar);
        }
    }
}
