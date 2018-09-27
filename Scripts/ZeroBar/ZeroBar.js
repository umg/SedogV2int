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
				return symbol + negative + (j ? i.substr(0, j) + thousand : "") + i.substr(j).replace(/(\d{3})(?=\d)/g, "$1" + thousand) + (places ? decimal + Math.abs(number - i).toFixed(places).slice(2) : "");
			};
			
			$.fn.ZeroMiddleBar = function (options) {
				options = $.extend({}, $.fn.ZeroMiddleBar.defaultOptions, options);
				
				$(this).each(function(){				
					
				    var
						canvas = this,
						fullWidth = 400,//valor para cálculo do percentual
						fullHeight = 20,
						top = 0,
						ctx = canvas.getContext('2d'),
						_parentPercent = $(this).parent().width() / fullWidth,
						h = fullHeight * _parentPercent,
						w = fullWidth * _parentPercent,
						total = options.total == 0 ? $(canvas).data("total") : options.total,
						atual = options.atual == 0 ? $(canvas).data("atual") : options.atual,
						percentual = $(canvas).data("percentual") * ($(canvas).data("atual") < 0 ? -1 : 1),//atual / total,
                        percentualBG = atual / total,
						text,
						animTotal = 0,
						speed = options.speed,
						mostrar = options.mostrar,
						name = $(canvas).data("name"),
						artist = $(canvas).data("artist"),
						mask = new Image(60, 60),
						url = "";

				    if (window.location.href.toUpperCase().indexOf("SEDOGV2INT") > -1)
				        url = "/SEDOGV2INT";
						
					canvas.width = w;
					canvas.height = h;
					
					canvas.onmousemove = function (e) {
						mostrar=true;
						ctx.clearRect(0, 0, canvas.width, canvas.height);
						ctx.save();
						draw();
					};

					canvas.onmouseout = function (e) {
					    mostrar = options.mostrar;
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
					    ctx.font = "11px Verdana";

					    if (animTotal >= 100) animTotal = 100;
						ctx.globalCompositeOperation = "source-over";
						
						ctx.save();
						ctx.fillStyle = "white";
						ctx.fillRect(0, 0, canvas.width, canvas.height);
						ctx.fillStyle = options.colorBg;
						ctx.fillRect((canvas.width / 2), top, ((canvas.width / 2) * percentualBG) * (animTotal / 100), (canvas.height) + (top));
						ctx.fillRect((canvas.width / 2), top, ((canvas.width / 2) * percentualBG * -1) * (animTotal / 100), (canvas.height) + (top));

						ctx.fillStyle = percentual<0?options.colorNegative:options.colorPositive;
						ctx.fillRect((canvas.width/2), top, ((canvas.width/2) * percentual) * (animTotal / 100), (canvas.height)+(top));
						
						ctx.fillStyle = options.colorBgBarGray;
						ctx.fillRect((canvas.width/2), top, ((canvas.width/2) * (percentual*-1)) * (animTotal / 100), (canvas.height)+(top));
						
						ctx.save();
						ctx.beginPath();
						ctx.strokeStyle = options.color;
						ctx.moveTo(canvas.width/2, top);
						ctx.lineTo(canvas.width/2, w);
						ctx.lineWidth = 1;
						ctx.stroke();
						
						ctx.globalCompositeOperation = "difference";
						ctx.save();
						ctx.fillStyle = "white";
						//ctx.font="12px Arial";
						//text = ctx.measureText("0");
						//ctx.fillText("0",canvas.width/2 - (text.width / 2), (top*_parentPercent-3));
							
						if (animTotal >= 100 && mostrar) {
							//ctx.font="14px Arial";
							//text = ctx.measureText(total.formatMoney());
							//ctx.fillText(total.formatMoney(),canvas.width - (text.width) - 5, 15 * _parentPercent);
							//text = ctx.measureText(atual.formatMoney());
							//ctx.font="12px Arial";
							//if (atual < 0) {
							//	ctx.fillText(atual.formatMoney(),(canvas.width / 2) - (text.width)-2, canvas.height-(5*_parentPercent));
							//}else{
							//	ctx.fillText(atual.formatMoney(),(canvas.width / 2) +2, canvas.height-(5*_parentPercent));
						    //}
						    text = ctx.measureText(name);
						    ctx.fillText(name, (canvas.width / 2) - (text.width) - 5, (canvas.height / 2) + 5);
						    text = ctx.measureText(artist);
						    ctx.fillText(artist, (canvas.width / 2) + 5, (canvas.height / 2) + 5);

						}
						ctx.globalCompositeOperation = "source-over";

						/*ctx.save();
						ctx.fillStyle = options.colorText;
						ctx.font="14px Arial";
						text = ctx.measureText(name);
						ctx.fillText(name, 4 , 15 * _parentPercent);*/


						if (atual < 0) {
						    if ((atual * -1) / total >= 1)
						    {
						        ctx.globalCompositeOperation = "difference";
						        ctx.save();
						        text = ctx.measureText(Math.round((atual / total) * 100) + "%");
						        ctx.fillText(Math.round((atual / total) * 100) + "%", (15 * _parentPercent) + 5, canvas.height - (5 * _parentPercent));
						        mask.src = url + '/Scripts/ZeroBar/stop.svg';
						        ctx.globalCompositeOperation = "source-over";
						        ctx.drawImage(mask, (_parentPercent), canvas.height - (3 * _parentPercent) - (15 * _parentPercent) + 1, (15 * _parentPercent), (15 * _parentPercent));
						    }
						} else {
						    if (atual / total >= 1)
						    {
						        ctx.globalCompositeOperation = "difference";
						        ctx.save();
						        text = ctx.measureText(Math.round((atual / total) * 100) + "%");
						        ctx.fillText(Math.round((atual / total) * 100) + "%", canvas.width - text.width - 5 - (16 * _parentPercent), canvas.height - (5 * _parentPercent));
						        mask.src = url + '/Scripts/ZeroBar/medal.svg';
						        ctx.globalCompositeOperation = "source-over";
						        ctx.drawImage(mask, canvas.width - (16 * _parentPercent), canvas.height - (3 * _parentPercent) - (15 * _parentPercent) + 1, (15 * _parentPercent), (15 * _parentPercent));
						    }
						}
						
						
					};
					
					if (canvas.getContext) {
						animate();
					}
					
				});
			};
			$.fn.ZeroMiddleBar.defaultOptions = {
				width: 0,
				total: 0,
				atual: 0,
				speed: 5,
				color: "#000000",
				colorLines: "#000000",
				colorBg: "#FAFAFA",
				colorBgBar: "RGBA(0,0,0,0.05)",
				colorBgBarGray: "#CFCFCF",
				colorText: "#000000",
				colorPositive: "rgba(0, 72, 70,1)",
				colorNegative: "rgba(255, 165, 0,1)",
				colorLimitText: "#FFFFFF",
				corlorLimite: "#FF0000",
				colorTextTotal: "#000000",
				type: "",
				mostrar:true
			};
		}(jQuery));