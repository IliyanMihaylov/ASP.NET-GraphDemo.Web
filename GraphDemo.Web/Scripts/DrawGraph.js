window.onload = function () {
    ShowGraph('/graph');
};

function ShowGraph(path) {
    var width = 960,
        height = 300

    var graphDiv = document.getElementById("graph");

    while (graphDiv.hasChildNodes()) {
        graphDiv.removeChild(graphDiv.lastChild);
    }

    var svg = d3.select("#graph").append("svg")
            .attr("width", "100%").attr("height", "100%")
            .attr("pointer-events", "all");

    var force = d3.layout.force()
        .distance(30)
        .linkDistance(60)
        .charge(-200)
        .size([width, height]);

    d3.json(path, function (error, graph) {
        if (error) throw error;

        force
            .nodes(graph.nodes)
            .links(graph.links)
            .start();

        var link = svg.selectAll(".link")
            .data(graph.links)
            .enter().append("line")
            .attr("class", "link");

        var node = svg.selectAll(".node")
            .data(graph.nodes)
            .enter().append("g")
            .attr("class", "node")
            .call(force.drag);

        node.append("image")
            .attr("xlink:href", "http://legislatie.resurse-pentru-democratie.org/liblack.png")
            .attr("x", -8)
            .attr("y", -8)
            .attr("width", 22)
            .attr("height", 22);

        node.append("text")
            .attr("dx", 12)
            .attr("dy", ".35em")
            .text(function (d) { return d.name });

        force.on("tick", function () {
            link.attr("x1", function (d) { return d.source.x; })
                .attr("y1", function (d) { return d.source.y; })
                .attr("x2", function (d) { return d.target.x; })
                .attr("y2", function (d) { return d.target.y; });

            node.attr("transform", function (d) { return "translate(" + d.x + "," + d.y + ")"; });
        });

        addNodesForSearch(graph.nodes);
    });
}

function ShowFullGraph()
{
    ShowGraph('/graph');
}

function InsertNode() {
    var relation = $("#chooseRelation").text();
    var name = $("#nodeText").val();

    ShowGraph("/nodeInsert?name=" + name + "&link=" + relation);
}

function SearchButton() {
    var from = $("#fromCity").text();
    var to = $("#toCity").text();

    ShowGraph("/shortestPath?from=" + from + "&to=" + to);
}

function addNodesForSearch(nodes) {
    addNodesToInternalElement(nodes, "from");
    addNodesToInternalElement(nodes, "to");
    addNodesToInternalElement(nodes, "relation");
}

function addNodesToInternalElement(nodes, internalElementId) {
    var element = document.getElementById(internalElementId);

    while (element.hasChildNodes()) {
        element.removeChild(element.lastChild);
    }

    for (i = 0; i < nodes.length; i++) {
        var node = nodes[i];

        var liElement = document.createElement('li');
        var hrefElement = document.createElement("a");

        hrefElement.setAttribute("href", "#");
        hrefElement.innerHTML = node.name;
        hrefElement.addEventListener("click", function () {
            var selText = $(this).text();
            $(this).parents('.btn-group').find('.dropdown-toggle').html(selText + '<span class="caret"></span>');
        });

        liElement.appendChild(hrefElement);
        element.appendChild(liElement);
    }
}