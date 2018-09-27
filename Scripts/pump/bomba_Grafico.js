(function () {
    var lastTime = 0;
    var vendors = ['ms', 'moz', 'webkit', 'o'];
    for (var x = 0; x < vendors.length && !window.requestAnimationFrame; ++x) {
        window.requestAnimationFrame = window[vendors[x] + 'RequestAnimationFrame'];
        window.cancelAnimationFrame = window[vendors[x] + 'CancelAnimationFrame']
                                   || window[vendors[x] + 'CancelRequestAnimationFrame'];
    }

    if (!window.requestAnimationFrame)
        window.requestAnimationFrame = function (callback, element) {
            var currTime = new Date().getTime();
            var timeToCall = Math.max(0, 16 - (currTime - lastTime));
            var id = window.setTimeout(function () { callback(currTime + timeToCall); },
              timeToCall);
            lastTime = currTime + timeToCall;
            return id;
        };

    if (!window.cancelAnimationFrame)
        window.cancelAnimationFrame = function (id) {
            clearTimeout(id);
        };
}());

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

    function hex2rgb(hex) {
        var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
        return result ? {
            r: parseInt(result[1], 16),
            g: parseInt(result[2], 16),
            b: parseInt(result[3], 16),
            rgb: parseInt(result[1], 16) + ", " + parseInt(result[2], 16) + ", " + parseInt(result[3], 16)
        } : null;
    }

    $.fn.bombaCombustivel = function (options) {

			var settings = $.extend({
				width: 0,
				total: 0,
				atual: 0,
				provisao: 0,
				totalPlan: 0,
				provisaoPlan: 0,

				provisaoplan_mes: 0,
				atual_mes: 0,
				provisao_mes: 0,

				speed: 5,
				color: "#FFA500",
				colorText: "#000000",
				colorToal: "#FFFFFF",
				colorLimitText: "#FFFFFF",
				corlorLimite: "#FF0000",
				colorTextTotal: "#000000",
				drawGrid: false,
				mostrarSempre: true,
				type: "futuristic",
				plan: false,
                mensal: false
			}, options);

			

			return $(this).each(function(){			
				
			settings.atual = $(this).data("atual");
			settings.total = $(this).data("total");
			settings.provisao = $(this).data("provisao");
			settings.totalPlan = $(this).data("totalplan");
			settings.provisaoPlan = $(this).data("provisaoplan");

			settings.atual_mes = $(this).data("atual_mes");
			settings.provisaoplan_mes = $(this).data("provisaoplan_mes");
			settings.provisao_mes = $(this).data("provisao_mes");
				
			var _elements = $(this).closest('.grafico').find('.tanque').length;

			var url = "";
			var minWidth = 100;
			var maxWidth = 160;
			var fullWidth = 800;//812 é o valor da largura do maximo de tela hj
			var fontSize = 11;
			var _padding = 12;
			var _parentPercent = $(this).parent().parent().width() / fullWidth;
			

			var widthOne = $(this).parent().parent().width() / _elements;

			var _parent = $(this).parent().parent();

			var rgb = hex2rgb(settings.color);
			var canvas = this;

			if (settings.width == 0) {
				canvas.width = 150;
			} else {
				canvas.width = settings.width;
			}
			var canvasWidth = canvas.width;

			canvas.width = maxWidth * _parentPercent;
			if (maxWidth * _parentPercent > maxWidth) canvas.width = maxWidth;
			if (maxWidth * _parentPercent < minWidth) canvas.width = minWidth;

			canvas.width = Math.round(widthOne, 0) - _padding;
			if (canvas.width > maxWidth) canvas.width = maxWidth;

			canvas.height = canvas.width * 2;
			var alpha = 0.5;
			if (settings.mostrarSempre)
				alpha = 1;

			//console.log(canvas.width);
			var
				bw = canvas.width,
				bh = canvas.height,
				p = 0,
				cemPorCento = canvas.height, //- ((canvas.height-(canvas.height/2)) / 2),		
				total = settings.total,
				atual = settings.atual,
				provisao = settings.provisao,
				percProv = provisao / total,
				percAtual = atual / total,
				ctx = canvas.getContext('2d'),
				animTotal = 0,
				speed = settings.speed,
				rects = [
					{ x: 0, y: ((canvas.height) - (canvas.height * percAtual)), w: canvas.width, h: (canvas.height * percAtual) - (canvas.width * 0.2), R: rgb.r, G: rgb.g, B: rgb.b, A: alpha, v: atual }
				],
				line = [{ x: 0, y: ((canvas.height) - (canvas.height * percProv)), w: canvas.width }],
				i = 0,
				r,
				text,
				legenda,
				canvasOffset = $(canvas).offset(),
				offsetX = canvasOffset.left,
				offsetY = canvasOffset.top;
				

				if (settings.plan) {
					if (!settings.mensal) {
					    total = settings.totalPlan;
					    atual = settings.atual;
					    provisao = settings.provisaoPlan;
					    percProv = provisao / total;
					    percAtual = atual / total;
					}
					else {
					    total = settings.provisaoplan_mes;
					    atual = settings.atual_mes;
					    provisao = settings.provisaoplan_mes;
					    percProv = provisao / total;
					    percAtual = atual / total;
					}
				} else {
				    if (!settings.mensal) {
				        total = settings.total;
				        atual = settings.atual;
				        provisao = settings.provisao;
				        percProv = provisao / total;
				        percAtual = atual / total;
				    } else {
				        total = settings.provisao_mes;
				        atual = settings.atual_mes;
				        provisao = settings.provisao_mes;
				        percProv = provisao / total;
				        percAtual = atual / total;
				    }
				}

				rects = [{ x: 0, y: ((canvas.height) - (canvas.height * percAtual)), w: canvas.width, h: (canvas.height * percAtual) - (canvas.width * 0.2), R: rgb.r, G: rgb.g, B: rgb.b, A: alpha, v: atual }];
				line = [{ x: 0, y: ((canvas.height) - (canvas.height * percProv)), w: canvas.width }];
				
			var mask = new Image();
			var mDf = $.Deferred();

			if (window.location.href.toUpperCase().indexOf("SEDOGV2INT") > -1)
			    url = "/SEDOGV2INT";

			switch (settings.type) {
				case "futuristic":
					mask.src = url + '/Scripts/pump/mask_futuristic.svg';
					break;
				case "":
				default:
					mask.src = url + '/Scripts/pump/mask_default.svg';
					break;
			}

			mask.addEventListener("load", function () { mDf.resolve(this); });
			$.when(mDf).done(function () {
				animate();
			});

			canvas.onmousemove = function (e) {
				ctx.save();

				canvasOffset = $(canvas).offset(),
				offsetX = canvasOffset.left,
				offsetY = canvasOffset.top;

				var rect = this.getBoundingClientRect(),
				x = e.clientX - rect.left,
				y = e.clientY - rect.top,
				i = 0, r;

				ctx.clearRect(0, 0, canvas.width, canvas.height);
				draw(parseInt(e.clientX - offsetX), parseInt(e.clientY - offsetY));

				//console.log(parseInt(e.clientX - offsetX) + " - " + parseInt(e.clientY - offsetY));
			};

			canvas.onmouseout = function (e) {
				ctx.clearRect(0, 0, canvas.width, canvas.height);
				ctx.save();
				draw(null, null);

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

			function draw(mouseX, mouseY) {
				
			    _parentPercent = _parent.width() / fullWidth;

			    //if (_parentPercent < minParentPercent) _parentPercent = minParentPercent;
			    
			    canvas.width = maxWidth * _parentPercent;
			    if (maxWidth * _parentPercent > maxWidth) canvas.width = maxWidth;
			    if (maxWidth * _parentPercent < minWidth) canvas.width = minWidth;

			    canvas.width = Math.round(widthOne, 0) - _padding;
			    if (canvas.width > maxWidth) canvas.width = maxWidth;

				canvas.height = canvas.width * 2;
				cemPorCento = canvas.height;
				line[0].y = (canvas.height - (canvas.width * 0.2)) * percProv;
				rects[0].h = (canvas.height * percAtual) - (canvas.width * 0.1);
				
				var area = canvas.height - (canvas.width * 0.2);
				rects[0].h = area * percAtual;

				var mostrarSempre = false;
				if (canvas.getContext) {
					if (animTotal >= 100) {
						animTotal = 100;
						if (settings.mostrarSempre)
							mostrarSempre = true;
					}
                    
					ctx.font = "bold " + Math.floor(fontSize) + "px Arial";

					ctx.globalCompositeOperation = "source-over";
					ctx.drawImage(mask, 0, 0, canvas.width, canvas.height);
					//ctx.globalCompositeOperation = "destination-over";
					i = 0;
					r = rects[0];

					ctx.beginPath();
					ctx.moveTo(0, animTotal);
					/*for (var x = 0; x <= r.w+50; x += 20) {
						ctx.quadraticCurveTo(x, r.y+((cemPorCento-r.y)*((100-animTotal)/100)), x, r.y+((cemPorCento-r.y)*((100-animTotal)/100)));
					}
					ctx.lineTo(r.w, cemPorCento);
					ctx.lineTo(0, cemPorCento);
					ctx.closePath();*/
					//console.log(r.h);

					ctx.fillStyle = ctx.fillStyle = 'rgba(' + r.R + ',' + r.G + ',' + r.B + ',' + r.A + ')';
					//ctx.fillRect((canvas.width * 0.7), (canvas.height - (canvas.width * 0.1)) - ((r.h - ((canvas.width * 0.1))) * (animTotal / 100)), (canvas.width * 0.2), (r.h - ((canvas.width * 0.1))) * (animTotal / 100));
					ctx.fillRect((canvas.width * 0.7), (canvas.height - (canvas.width * 0.1)) - (r.h * (animTotal / 100)), (canvas.width * 0.2), r.h * (animTotal / 100));

					if ((mouseX || mouseY) || mostrarSempre) {
						if ((mouseY >= (canvas.height - (canvas.width * 0.1)) - (r.h * (animTotal / 100)) || percAtual < 0.1) || mostrarSempre) {
							//if ((mouseY >= r.y || percAtual < 0.1) || mostrarSempre) {
							ctx.save();
							ctx.fillStyle = 'rgba(' + r.R + ',' + r.G + ',' + r.B + ',' + 1 + ')';
							//ctx.fillRect((canvas.width * 0.7), (canvas.height - (canvas.width * 0.1)) - ((r.h - ((canvas.width * 0.1))) * (animTotal / 100)), (canvas.width * 0.2), (r.h - ((canvas.width * 0.1))) * (animTotal / 100));
							ctx.fillRect((canvas.width * 0.7), (canvas.height - (canvas.width * 0.1)) - (r.h * (animTotal / 100)), (canvas.width * 0.2), r.h * (animTotal / 100));

							ctx.globalCompositeOperation = "source-over";

							ctx.beginPath();
							ctx.strokeStyle = settings.color;
							ctx.moveTo((canvas.width * 0.1), canvas.height - ((canvas.width * 0.1) + 1));
							ctx.lineTo(canvas.width * 0.8, canvas.height - ((canvas.width * 0.1) + 1));
							ctx.lineWidth = 3;
							ctx.stroke();

							ctx.fillStyle = settings.color;
							ctx.fillRect((canvas.width * 0.1) - ((canvas.width * 0.1) / 2), canvas.height - (canvas.width * 0.3) + 3, ctx.measureText(r.v.formatMoney()).width + (canvas.width * 0.1), (canvas.width * 0.2));

							ctx.save();

							ctx.fillStyle = settings.colorText;
							ctx.fillText(r.v.formatMoney(), (canvas.width * 0.1), (canvas.height - (canvas.width * 0.3)) + ((canvas.width * 0.15)));
						} else {
							ctx.fillStyle = 'rgba(' + r.R + ',' + r.G + ',' + r.B + ',' + r.A + ')';
							ctx.fill();
						}
					}
					else {
						ctx.fillStyle = 'rgba(' + r.R + ',' + r.G + ',' + r.B + ',' + r.A + ')';
						ctx.fill();
					}

					ctx.globalCompositeOperation = "source-over";
					ctx.beginPath();
					ctx.moveTo(canvas.width * 0.7, (canvas.height) - line[0].y - (canvas.width * 0.1));
					ctx.lineTo(canvas.width * 0.9, (canvas.height) - line[0].y - (canvas.width * 0.1));
					ctx.strokeStyle = settings.corlorLimite;
					ctx.lineWidth = 3;
					ctx.stroke();

					if ((mouseX || mouseY) || mostrarSempre) {
						ctx.globalCompositeOperation = "source-over";
						ctx.save();

						ctx.beginPath();
						ctx.strokeStyle = settings.colorToal;
						ctx.moveTo((canvas.width * 0.1), (canvas.width * 0.1) + 1);
						ctx.lineTo(canvas.width * 0.7, (canvas.width * 0.1) + 1);
						ctx.lineWidth = 3;
						ctx.stroke();

						text = ctx.measureText(total.formatMoney());

						ctx.fillStyle = settings.colorToal;
						ctx.fillRect((canvas.width * 0.1) - ((canvas.width * 0.1) / 2), (canvas.width * 0.1) - 1, text.width + (canvas.width * 0.1), (canvas.width * 0.2));
						ctx.fillStyle = settings.colorTextTotal;
						ctx.fillText(total.formatMoney(), (canvas.width * 0.1), (canvas.width * 0.2) + ((canvas.width * 0.1) / 3));

						if ((mouseY >= (canvas.height) - line[0].y - (canvas.width * 0.1) - 10 && mouseY <= (canvas.height) - line[0].y - (canvas.width * 0.1) + 10)) {
							ctx.save();
							ctx.beginPath();

							legenda = provisao.formatMoney();
							text = ctx.measureText(legenda);
							ctx.globalCompositeOperation = "source-over";
							ctx.fillStyle = settings.corlorLimite;
							ctx.strokeStyle = settings.corlorLimite;
							//ctx.moveTo((canvas.width * 0.1), line[0].y);
							//ctx.lineTo(canvas.width * 0.9, line[0].y);

							ctx.moveTo(canvas.width * 0.1, (canvas.height) - line[0].y - (canvas.width * 0.1));
							ctx.lineTo(canvas.width * 0.9, (canvas.height) - line[0].y - (canvas.width * 0.1));

							ctx.lineWidth = 3;
							ctx.stroke();
							ctx.fillRect((canvas.width * 0.1) - ((canvas.width * 0.1) / 2), ((canvas.height) - line[0].y - (canvas.width * 0.1)) - (canvas.width * 0.1), text.width + (canvas.width * 0.1), (canvas.width * 0.2));
							ctx.fillStyle = settings.colorLimitText;
							ctx.fillText(legenda, (canvas.width * 0.1), (canvas.height) - line[0].y - (canvas.width * 0.1) + ((canvas.width * 0.1) / 3));
						}
					}
				}
				if (settings.drawGrid)
					drawBoard();
			}

			function drawBoard() {

				ctx.globalCompositeOperation = "destination-over";//escreve somente onde não existem pixels
				for (var x = 0; x <= bw; x += 10) {
					ctx.moveTo(0.5 + x + p, p);
					ctx.lineTo(0.5 + x + p, bh + p);
				}

				for (var x = 0; x <= bh; x += 10) {
					ctx.moveTo(p, 0.5 + x + p);
					ctx.lineTo(bw + p, 0.5 + x + p);
				}
				ctx.lineWidth = 1;
				ctx.strokeStyle = "#DDDDDD";
				ctx.stroke();
			}
			
		
			/*return {
					update: function () {
						_parentPercent = _parent.width() / fullWidth;
						canvas.width = canvasWidth * _parentPercent;
						canvas.height = canvas.width * 2;
						cemPorCento = canvas.height;
						line[0].y = ((canvas.height) - (canvas.height * percProv));
						rects[0].h = (canvas.height * percAtual) - (canvas.width * 0.1);
						draw(null, null);
					},
					redraw: function (plan) {
						
						if (plan) {
							total = settings.totalPlan;
							atual = settings.atual;
							provisao = settings.provisaoPlan;
							percProv = provisao / total;
							percAtual = atual / total;
						} else {
							total = settings.total;
							atual = settings.atual;
							provisao = settings.provisao;
							percProv = provisao / total;
							percAtual = atual / total;
						}
						rects = [{ x: 0, y: ((canvas.height) - (canvas.height * percAtual)), w: canvas.width, h: (canvas.height * percAtual) - (canvas.width * 0.1), R: rgb.r, G: rgb.g, B: rgb.b, A: alpha, v: atual }];
						line = [{ x: 0, y: ((canvas.height) - (canvas.height * percProv)), w: canvas.width }];
						animTotal = 0;
						animate();
					}
			};*/
			
		});	
		
    };
}(jQuery));

