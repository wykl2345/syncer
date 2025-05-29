from fastapi import FastAPI, UploadFile, File, Form
from fastapi.responses import FileResponse
from pathlib import Path
import shutil
import os

app = FastAPI()
STORAGE_DIR = Path("storage")

@app.post("/upload")
async def upload_file(file: UploadFile = File(...), device_id: str = Form(...)):
    device_folder = STORAGE_DIR / file.filename
    device_folder.mkdir(parents=True, exist_ok=True)
    save_path = device_folder / f"{device_id}_{file.filename}"
    with open(save_path, "wb") as f:
        shutil.copyfileobj(file.file, f)
    return {"message": "Upload successful"}

@app.get("/download")
async def download_file(filename: str):
    device_folder = STORAGE_DIR / filename
    if not device_folder.exists():
        return {"error": "File not found"}
    # 默认返回最新的一个版本
    files = sorted(device_folder.glob("*"), key=os.path.getmtime, reverse=True)
    if not files:
        return {"error": "No versions available"}
    return FileResponse(files[0])
