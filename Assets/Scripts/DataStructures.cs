public enum Instrument
{
    Flute,
    Guitar,
    Tuba,
}

static class InstrumentMethods
{
    public static int ToInt(this Instrument instrument)
    {
        return (int) instrument;
    }
}