<%@ Page Language="VB" %>
<%
Response.Write(System.Security.Principal.WindowsIdentity.GetCurrent().Name)
%>
<%@ Page Language="C#" %> 
<% 
Response.Write(System.Security.Principal.WindowsIdentity.GetCurrent().Name); 
%>