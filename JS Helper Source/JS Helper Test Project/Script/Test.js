debugger
for (const table of document.getElementsByTagName("table")) {
	table.style.tableLayout = "fixed"
	table.style.border = "1px solid " + getColour()
	for (const row of table.rows) {
		for (const cel of row.cells) {
			cel.style.border = "1px solid " + getColour()
		}
	}
}

function getColour() {
	return choose(1 + Math.random() * 5, ["red", "green", "gold", "blue", "orange"])
}

function choose(n, l) {
	return l[Math.round(n) - 1]
}