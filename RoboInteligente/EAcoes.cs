using System.ComponentModel;

namespace RoboInteligente
{
    public enum EAcoes
    {
        [Description("Mover vazio")]
        MOVER_VAZIO = 1,
        [Description("Mover cheio")]
        MOVER_CHEIO = 2,
        [Description("Pegar item")]
        PEGAR_ITEM = 1,
        [Description("Largar item")]
        LARGAR_ITEM = 0
    }
}
