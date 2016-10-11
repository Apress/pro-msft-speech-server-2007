<%@ Register TagPrefix="speech" Namespace="Microsoft.Speech.Web.UI" Assembly="Microsoft.Speech.Web, Version=2.0.3400.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" >

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Untitled Page</title>
</head>

<body>
    <form id="form1" runat="server">

        <script src="Prompts/prompts.pf" type="text/jscript"></script>
    <div>
        <speech:answercall ID="AnswerCall1" runat="server">
        </speech:answercall>
        <speech:SemanticMap ID="SemanticMap1" runat="server" height="0px"><speech:SemanticItem id="productSize" runat="server" __designer:wfdid="w49"></speech:SemanticItem> <speech:SemanticItem id="productName" runat="server" __designer:wfdid="w50" BindOnChanged="True"></speech:SemanticItem> <speech:SemanticItem id="productColor" runat="server" __designer:wfdid="w51"></speech:SemanticItem></speech:SemanticMap>
        <speech:QA ID="sayGreeting" runat="server" PlayOnce="True">
            <Dtmf ID="sayGreeting_Dtmf">
            </Dtmf>
            <Prompt ID="sayGreeting_Prompt" InlinePrompt="Welcome to A B C Company Ordering System">
            </Prompt>
            <Reco ID="sayGreeting_Reco">
            </Reco>
        </speech:QA>
    </div>
        <speech:QA ID="askProduct" runat="server">
            <Dtmf ID="askProduct_Dtmf">
            </Dtmf>
            <Prompt ID="askProduct_Prompt" InlinePrompt="Would you like to order a Gadget, Sprocket or Widget?">
            </Prompt>
            <Answers>
<speech:Answer runat="server" SemanticItem="productName" XpathTrigger="\SML"></speech:Answer>
</Answers>
            <Reco ID="askProduct_Reco"><Grammars>
<speech:Grammar runat="server" ID="askProduct_Grammar1" Src="Grammars/Library.grxml#Product"></speech:Grammar>
</Grammars>
</Reco>
        </speech:QA>
        <speech:QA ID="askSize" runat="server">
            <Dtmf ID="askSize_Dtmf">
            </Dtmf>
            <Prompt ID="askSize_Prompt" InlinePrompt="Do you want that in small, medium or large?">
            </Prompt>
            <Answers>
<speech:Answer runat="server" SemanticItem="productSize" XpathTrigger="\SML"></speech:Answer>
</Answers>
            <Reco ID="askSize_Reco"><Grammars>
<speech:Grammar runat="server" ID="askSize_Grammar1" Src="Grammars/Library.grxml#Size"></speech:Grammar>
</Grammars>
</Reco>
        </speech:QA>
        <speech:QA ID="askColor" runat="server">
            <Dtmf ID="askColor_Dtmf">
            </Dtmf>
            <Prompt ID="askColor_Prompt" InlinePrompt="Do you want that in Red, White or Blue?">
            </Prompt>
            <Answers>
<speech:Answer runat="server" SemanticItem="productColor" XpathTrigger="\SML"></speech:Answer>
</Answers>
            <Reco ID="askColor_Reco"><Grammars>
<speech:Grammar runat="server" ID="askColor_Grammar1" Src="Grammars/Library.grxml#Color"></speech:Grammar>
</Grammars>
</Reco>
        </speech:QA>
    </form>
</body>
</html>
