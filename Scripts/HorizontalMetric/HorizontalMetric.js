(function ($) {
    Number.prototype.formatMoney = function (places, symbol, thousand, decimal, divided) {
        places = !isNaN(places = Math.abs(places)) ? places : 0;
        symbol = symbol !== undefined ? symbol : "$";
        thousand = thousand || ".";
        decimal = decimal || ",";
        divided = divided || 1000;
        var number = this,
            negative = number < 0 ? "-" : "",
            i = parseInt(number = Math.abs(+number || 0).toFixed(places), 10) + "",
            j = (j = i.length) > 3 ? j % 3 : 0;
        return negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
    };

    $.fn.MetricHorizontalBar = function (options) {
        options = $.extend({}, $.fn.MetricHorizontalBar.defaultOptions, options);

        $(this).each(function () {

            var
                canvas = this,
                fullWidth = 300,//valor para cálculo do percentual
                fullHeight = 20,
                top = 20,
                ctx = canvas.getContext('2d'),
                _parentPercent = $(this).parent().width() / fullWidth,
                h = fullHeight * _parentPercent,
                w = fullWidth * _parentPercent,
                totalfcast = options.totalfcast == 0 ? $(canvas).data("totalfcast") : options.totalfcast,
                totalplan = options.totalplan == 0 ? $(canvas).data("totalplan") : options.totalplan,
                atual = options.atual == 0 ? $(canvas).data("atual") : options.atual,
                plan = options.plan == 0 ? $(canvas).data("plan") : options.plan,
                fcast = options.fcast == 0 ? $(canvas).data("fcast") : options.fcast,
                isfplan = options.isPlan,
                total = isfplan ? totalplan : totalfcast,
                redline = (isfplan ? plan : fcast) / total,
                limite = (isfplan ? plan : fcast),
                percentual = atual / total,
                text,
                animTotal = 0,
                speed = options.speed,
                mostrar = options.mostrar,
                name = $(canvas).data("name"),
                mask = new Image(60, 60),
                colorText = $(canvas).data("color"),
                colorFill = $(canvas).data("barcolor");


            canvas.width = w;
            canvas.height = h;

            canvas.onmousemove = function (e) {
                mostrar = true;
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                ctx.save();
                draw();
            };

            canvas.onmouseout = function (e) {
                mostrar = false;
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                ctx.save();
                draw();
            };

            function animate() {
                if (animTotal < 100) {
                    window.requestAnimationFrame(animate);
                }
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                ctx.save();
                draw(null, null);
                animTotal = animTotal + speed;
            }

            function draw() {
                if (animTotal >= 100) animTotal = 100;
                ctx.globalCompositeOperation = "source-over";
                
                ctx.fillStyle = options.colorBg;
                ctx.fillRect(0, 0, canvas.width, canvas.height);
                ctx.fillStyle = options.colorBgBar;
                ctx.fillRect(0, 0, canvas.width, canvas.height);

                ctx.fillStyle = colorFill;
                ctx.fillRect(2, 2, ((canvas.width) * percentual) * (animTotal / 100), (canvas.height) - 4);

                /*ctx.lineWidth = 1;
                ctx.strokeStyle = options.colorLines;
                ctx.rect(2, 2, canvas.width - 4, canvas.height - 4);
                ctx.stroke();

                /*for (i = 0; i < 50; i++) {
                    ctx.save();
                    ctx.beginPath();
                    ctx.strokeStyle = options.corlorLimite;
                    ctx.moveTo(((canvas.width / 50) * i) + 2, canvas.height - 5 - (i % 5 == 0 ? 5 : 0));
                    ctx.lineTo(((canvas.width / 50) * i) + 2, canvas.height - 2);
                    ctx.lineWidth = 1;
                    ctx.stroke();
                }*/

                ctx.beginPath();
                ctx.strokeStyle = options.corlorLimite;
                ctx.moveTo((canvas.width) * redline, 2);
                ctx.lineTo((canvas.width) * redline, h - 2);
                ctx.lineWidth = 1;
                ctx.stroke();
                ctx.strokeStyle = options.color;
                ctx.fillStyle = colorText;

                ctx.font = "14px Arial";
                ctx.globalCompositeOperation = "difference";
                text = ctx.measureText(total.formatMoney());
                /*ctx.fillStyle = options.colorLines;
                ctx.fillRect((canvas.width) - (text.width) - 9, 2, text.width + 7, 20);*/
                ctx.fillStyle = "white";
                ctx.fillText(total.formatMoney(), canvas.width - (text.width) - 5, (canvas.height / 2) + 7);

                ctx.font = "14px Arial";
                text = ctx.measureText(atual.formatMoney());
                ctx.fillText(atual.formatMoney(), 6, (canvas.height / 2) + 7);

                if (animTotal >= 100 && mostrar) {
                    ctx.globalCompositeOperation = "source-over";
                    ctx.font = "14px Arial";
                    text = ctx.measureText(limite.formatMoney());
                    ctx.fillStyle = options.corlorLimite;
                    ctx.fillRect((canvas.width) * redline - (text.width / 2) - 2, (canvas.height / 2) - 14, text.width + 4, 28);
                    ctx.fillStyle = "white";
                    ctx.globalCompositeOperation = "difference";
                    ctx.fillText(limite.formatMoney(), (canvas.width) * redline - (text.width / 2), (canvas.height / 2) + (6));
                }

                

            };

            if (canvas.getContext) {
                animate();
            }

        });
    };
    $.fn.MetricHorizontalBar.defaultOptions = {
        width: 0,
        totalplan: 0,
        totalfcast: 0,
        plan: 0,
        fcast: 0,
        atual: 0,
        speed: 5,
        color: "#000000",
        colorLines: "#000000",
        colorBg: "#FFFFFF",
        colorBgBar: "RGBA(0,0,0,0.05)",
        colorBgBarGray: "#AAAAAA",
        colorText: "#000000",
        colorPositive: "#8888FF",
        colorFill: "rgba(255, 165, 0,1)",
        colorNegative: "#FF0000",
        colorLimitText: "#FFFFFF",
        corlorLimite: "#FF0000",
        colorTextTotal: "#000000",
        type: "",
        isPlan: false,
        mostrar: false
    };
}(jQuery));