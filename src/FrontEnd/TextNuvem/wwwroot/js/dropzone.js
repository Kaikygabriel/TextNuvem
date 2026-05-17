export function registerDropZone(element, dotnetRef) {
    element.addEventListener("drop", async (e) => {
        e.preventDefault();

        const files = e.dataTransfer.files;

        for (let file of files) {
            const text = await file.text();

            await dotnetRef.invokeMethodAsync(
                "ReceiveFileFromDrop",
                file.name,
                text
            );
        }
    });

    element.addEventListener("dragover", (e) => {
        e.preventDefault();
    });
}