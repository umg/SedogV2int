
(function ($) {
    $.fn.MapaRegiao = function (options) {
        options = $.extend({}, $.fn.MapaRegiao.defaultOptions, options);

        $(this).each(function () {

            var
                canvas = this,
                fullWidth = 702.95801,//valor para cálculo do percentual
                fullHeight = 721.474,
                top = 0,
                ctx = canvas.getContext('2d'),
                _parentPercent = $(this).parent().width() / fullWidth,
                h = fullHeight * _parentPercent,
                w = fullWidth * _parentPercent,
                text,
                mostrar = options.mostrar,
                url = "",
                animTotal = 0,
                speed = options.speed,
                norte = $(canvas).data("norte"),
                nordeste = $(canvas).data("nordeste"),
                centrooeste = $(canvas).data("centrooeste"),
                sudeste = $(canvas).data("sudeste"),
                sul = $(canvas).data("sul"),
                exterior = $(canvas).data("exterior"),
                total = (norte + nordeste + centrooeste + sudeste + sul + exterior),
                showPercent = $(canvas).data("percent") == "True"
            ;

            var mask = new Image();

            var mDf = $.Deferred();

            if (window.location.href.toUpperCase().indexOf("SEDOGV2INT") > -1)
                url = "/SEDOGv2INT";

            canvas.width = w;
            canvas.height = h;
            //console.log(_parentPercent);

            function animate() {
                //console.log(animTotal);
                if (animTotal < 100) {
                    window.requestAnimationFrame(animate);
                }
                ctx.clearRect(0, 0, canvas.width, canvas.height);
                ctx.save();
                draw();
                animTotal = animTotal + speed;
            }

            function draw() {

                if (animTotal >= 100) animTotal = 100;

                ctx.imageSmoothingEnabled = false;
                ctx.drawImage(mask, (w / 2) - ((w * (animTotal / 100)) / 2), (h / 2) - ((h * (animTotal / 100)) / 2), w * (animTotal / 100), h * (animTotal / 100));

                var imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
                for (var i = 0; i < imageData.data.length; i += 4) {
                    if (imageData.data[i] + imageData.data[i + 1] + imageData.data[i + 2] != 0) {
                        imageData.data[i + 3] = 255 * (animTotal / 100); // alpha
                    }
                }
                ctx.putImageData(imageData, 0, 0);
                if (animTotal >= 100) {
                    //var p = ctx.getImageData(0, 0, 1, 1).data;
                    //highlightByColor(p, imageData);						
                    //data = mask.data;		

                    if (showPercent) {
                        if (total == 0) total = 1;

                        ctx.font = "bold " + Math.round(30 * _parentPercent, 0) + "px Arial";
                        text = ctx.measureText((Math.round((norte / total) * 10000) / 100) + '%');
                        ctx.fillText((Math.round((norte / total) * 10000) / 100) + '%', 267 * _parentPercent, 158 * _parentPercent);

                        text = ctx.measureText((Math.round((nordeste / total) * 10000) / 100) + '%');
                        ctx.fillText((Math.round((nordeste / total) * 10000) / 100) + '%', 556 * _parentPercent, 260 * _parentPercent);

                        text = ctx.measureText((Math.round((centrooeste / total) * 10000) / 100) + '%');
                        ctx.fillText((Math.round((centrooeste / total) * 10000) / 100) + '%', 330 * _parentPercent, 388 * _parentPercent);

                        text = ctx.measureText((Math.round((sudeste / total) * 10000) / 100) + '%');
                        ctx.fillText((Math.round((sudeste / total) * 10000) / 100) + '%', 490 * _parentPercent, 466 * _parentPercent);

                        text = ctx.measureText((Math.round((sul / total) * 10000) / 100) + '%');
                        ctx.fillText((Math.round((sul / total) * 10000) / 100) + '%', 340 * _parentPercent, 635 * _parentPercent);
                    }
                    else {
                        ctx.font = "bold " + Math.round(36 * _parentPercent, 0) + "px Arial";
                        text = ctx.measureText($(canvas).data("norte"));
                        ctx.fillText($(canvas).data("norte"), 267 * _parentPercent, 158 * _parentPercent);

                        text = ctx.measureText($(canvas).data("nordeste"));
                        ctx.fillText($(canvas).data("nordeste"), 556 * _parentPercent, 260 * _parentPercent);

                        text = ctx.measureText($(canvas).data("centrooeste"));
                        ctx.fillText($(canvas).data("centrooeste"), 330 * _parentPercent, 388 * _parentPercent);

                        text = ctx.measureText($(canvas).data("sudeste"));
                        ctx.fillText($(canvas).data("sudeste"), 504 * _parentPercent, 466 * _parentPercent);

                        text = ctx.measureText($(canvas).data("sul"));
                        ctx.fillText($(canvas).data("sul"), 380 * _parentPercent, 600 * _parentPercent);
                    }

                }
            };

            mask.src = url + '/Scripts/MapaRegiao/brasil2.svg';

            mask.addEventListener("load", function () { mDf.resolve(this); });
            $.when(mDf).done(function () {
                animate();
            });

            canvas.onmousemove = function (e) {
                if (animTotal >= 100) {
                    var pos = getMousePos(canvas, e);
                    var p = ctx.getImageData(pos.x, pos.y, 1, 1).data;
                    var imageData = ctx.getImageData(0, 0, canvas.width, canvas.height);
                    //console.log(imageData.data);
                    //console.log(p);
                    highlightByColor(p, imageData);
                    //console.log('posX: ' + pos.x + ' - posY: '  + pos.y);
                    //draw(pos.x, pos.y);
                }
            };

            function getMousePos(canvas, evt) {
                var rect = canvas.getBoundingClientRect();
                return {
                    x: evt.clientX - rect.left,
                    y: evt.clientY - rect.top
                };
            }

            canvas.onmouseout = function (e) {
                draw();
            }
            function highlightByColor(p, imageData) {

                if (p[3] != 200) {
                    //console.log('r:' + p[0] + ' g:' + p[1] + ' b:' +  p[2]);
                    for (var i = 0; i < imageData.data.length; i += 4) {

                        if (p[0] == imageData.data[i] && p[1] == imageData.data[i + 1] && p[2] == imageData.data[i + 2]) {
                            //imageData.data[i]     = 255 - imageData.data[i];     // red
                            //imageData.data[i + 1] = 255 - imageData.data[i + 1]; // green
                            //imageData.data[i + 2] = 255 - imageData.data[i + 2]; // blue
                            if (imageData.data[i] + imageData.data[i + 1] + imageData.data[i + 2] != 0) {
                                imageData.data[i + 3] = 200; // alpha
                            }
                        }
                        else {
                            if (imageData.data[i] + imageData.data[i + 1] + imageData.data[i + 2] != 0) {
                                imageData.data[i + 3] = 255; // alpha
                            }
                        }

                    }
                    ctx.putImageData(imageData, 0, 0);
                }
            }
        });
    };
    $.fn.MapaRegiao.defaultOptions = {
        width: 0,
        color: "#000000",
        colorLines: "#000000",
        colorBg: "#FAFAFA",
        colorBgBar: "RGBA(0,0,0,0.05)",
        colorBgBarGray: "#CFCFCF",
        colorText: "#000000",
        colorPositive: "rgba(0, 72, 70,1)",
        colorNegative: "#FF0000",
        colorLimitText: "#FFFFFF",
        corlorLimite: "#FF0000",
        colorTextTotal: "#000000",
        type: "",
        speed: 10,
        mostrar: true
    };
}(jQuery));