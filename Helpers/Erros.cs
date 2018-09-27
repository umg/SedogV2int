using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SEDOGv2.Helpers
{
    public static class Erros
    {

        public enum MessageType
        {
            NOTYPE,
            ERROR,
            SUCCESS
        }

        public static string ShowMessage(MessageType type, string message)
        {
            string _html = "";
            switch (type)
            {
                case MessageType.NOTYPE:
                    _html = string.Concat("<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Aviso!</strong> ", message, "</div>");
                    break;
                case MessageType.ERROR:
                    _html = string.Concat("<div class='alert alert-danger alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Erro!</strong> ", message, "</div>");
                    break;
                case MessageType.SUCCESS:
                    _html = string.Concat("<div class='alert alert-success alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Tudo certo!</strong> ", message, "</div>");
                    break;
                default:
                    _html = string.Concat("<div class='alert alert-warning alert-dismissible' role='alert'><button type='button' class='close' data-dismiss='alert' aria-label='Close'><span aria-hidden='true'>&times;</span></button><strong>Aviso!</strong> ", message, "</div>");
                    break;
            }


            return _html;
        }
    }
}