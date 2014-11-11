namespace TelephoneSystem.ATSModel
{
    public enum TerminalState
    {
        InitialState,//трубка положена, исходное состояние
        OutboundSet, //исходящий набор
        ConversationalState,//состояние разговора
    }
}
