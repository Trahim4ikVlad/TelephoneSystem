namespace TelephoneSystem.ATSModel
{
    public enum TerminalState
    {
        InitialState,//трубка положена, исходное состояние
        ExpectationSet,//трубка снята, ожидание набора
        OutboundSet, //исходящий набор
        ConversationalState,//состояние разговора
        NewCallInConversationState//приём нового вызова в состоянии разговора
    }
}
