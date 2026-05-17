import JSZip from "https://cdn.jsdelivr.net/npm/jszip@3.10.1/+esm";

export async function downloadProjectZip(projectName, files) {
    const zip = new JSZip();

    for (const file of files) {
        zip.file(file.path, file.content);
    }

    const blob = await zip.generateAsync({ type: "blob" });

    const url = URL.createObjectURL(blob);

    const a = document.createElement("a");
    a.href = url;
    a.download = projectName + ".zip";
    a.click();

    URL.revokeObjectURL(url);
}