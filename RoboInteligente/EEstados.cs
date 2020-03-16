using System.ComponentModel;

namespace RoboInteligente
{
    public enum EEstados
    {
        [Description(" ")]
        VAZIO = 0,
        [Description("R")]
        ROBO = 1,
        [Description("M")]
        MATERIAL = 2,
        [Description("P")]
        COLETA = 3
    }
}
