using System;
using System.Reflection;

namespace ServicoRest_AplicandoSerti_BT.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}