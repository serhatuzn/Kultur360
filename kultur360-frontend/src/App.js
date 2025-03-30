import React, { useEffect, useState, useRef } from "react";
import { MapContainer, TileLayer, Marker, Popup, useMap } from "react-leaflet";
import axios from "axios";
import "leaflet/dist/leaflet.css";
import L from "leaflet";
import "./App.css";
import "bootstrap/dist/css/bootstrap.min.css";

// Leaflet ikon ayarları
delete L.Icon.Default.prototype._getIconUrl;
L.Icon.Default.mergeOptions({
  iconRetinaUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon-2x.png",
  iconUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-icon.png",
  shadowUrl: "https://cdnjs.cloudflare.com/ajax/libs/leaflet/1.7.1/images/marker-shadow.png",
});

const sehirler = {
  "": null,
  İstanbul: [41.0082, 28.9784],
  Ankara: [39.9208, 32.8541],
  İzmir: [38.4192, 27.1287],
  Konya: [37.8746, 32.4932],
  Trabzon: [41.0015, 39.7178],
  Antalya: [36.8969, 30.7133],
  Şanlıurfa: [37.1606, 38.7969],
};

const kategoriler = ["Hepsi", "Tarihi Yer", "Restoran", "Müze"];

function App() {
  const [yerler, setYerler] = useState([]);
  const [konum, setKonum] = useState(null);
  const [searchTerm, setSearchTerm] = useState("");
  const [secilenSehir, setSecilenSehir] = useState(null);
  const [aktifKategori, setAktifKategori] = useState("Hepsi");
  const mapRef = useRef();

  useEffect(() => {
    navigator.geolocation.getCurrentPosition(
      (pos) => {
        setKonum({
          latitude: pos.coords.latitude,
          longitude: pos.coords.longitude,
        });
      },
      (err) => console.error("Konum alınamadı:", err)
    );

    axios
      .get("http://localhost:5025/api/tarihiyer")
      .then((res) => setYerler(res.data))
      .catch((err) => console.error("Veri çekme hatası:", err));
  }, []);

  const filtrelenmisYerler = yerler.filter((yer) => {
    const aramaUygun = yer.isim.toLowerCase().includes(searchTerm.toLowerCase());
    const sehirUygun =
      !secilenSehir ||
      Object.entries(sehirler).find(([_, v]) => v === secilenSehir)?.[0] === yer.sehir;
    const kategoriUygun =
      aktifKategori === "Hepsi" || yer.kategori?.toLowerCase() === aktifKategori.toLowerCase();

    return aramaUygun && sehirUygun && kategoriUygun;
  });

  function ChangeMapView({ coords }) {
    const map = useMap();
    useEffect(() => {
      if (coords) {
        map.setView(coords, 13);
      }
    }, [coords, map]);
    return null;
  }

  return (
    <>
      {/* NAVBAR */}
      <nav
        className="navbar navbar-expand-lg navbar-dark py-2"
        style={{ background: "linear-gradient(to right,#1f1c2c,#928dab)" }}
      >
        <div className="container-fluid d-flex justify-content-between align-items-center">
          {/* Sol Kategoriler */}
          <div className="d-flex gap-2">
            {kategoriler.map((kategori, i) => (
              <button
                key={i}
                onClick={() => setAktifKategori(kategori)}
                className={`btn btn-sm ${aktifKategori === kategori ? "btn-warning" : "btn-outline-light"}`}
              >
                {kategori}
              </button>
            ))}
          </div>

          {/* Merkezde Logo */}
          <span className="navbar-brand fw-bold text-warning text-center">
            <h1 className="fs-2 fw-bold">Kültür360</h1>
          </span>

          {/* Sağ Arama/Şehir */}
          <form className="d-flex gap-2" onSubmit={(e) => e.preventDefault()}>
            <input
              className="form-control form-control-sm"
              type="search"
              placeholder="🔍 Yer ara"
              value={searchTerm}
              onChange={(e) => setSearchTerm(e.target.value)}
            />
            <select
              className="form-select form-select-sm"
              onChange={(e) => {
                const sehir = e.target.value;
                setSecilenSehir(sehirler[sehir]);
              }}
            >
              {Object.keys(sehirler).map((sehir) => (
                <option key={sehir} value={sehir}>
                  {sehir === "" ? "Şehir seçin" : sehir}
                </option>
              ))}
            </select>
          </form>
        </div>
      </nav>

      {/* Slogan Alanı */}
      <div className="text-center py-2" style={{ background: "#2a2a3b" }}>
        <h5 className="text-white m-0">
          🧭 Keşfet, Öğren, Yakınındaki Kültürü Gör!
        </h5>
      </div>

      {/* Harita */}
      <div className="position-relative" style={{ height: "calc(100vh - 96px)" }}>
        {secilenSehir && (
          <div className="position-absolute top-0 start-0 m-3 bg-light px-3 py-1 rounded shadow-sm">
            📌 {Object.entries(sehirler).find(([_, v]) => v === secilenSehir)?.[0]}
          </div>
        )}
        <div className="position-absolute bottom-0 start-0 m-3 bg-dark text-white px-3 py-1 rounded shadow-sm">
          🔍 {filtrelenmisYerler.length} yer bulundu
        </div>

        <MapContainer
          center={
            secilenSehir
              ? secilenSehir
              : konum
              ? [konum.latitude, konum.longitude]
              : [41.0082, 28.9784]
          }
          zoom={13}
          style={{ height: "100%", width: "100%" }}
          whenCreated={(mapInstance) => (mapRef.current = mapInstance)}
        >
          <ChangeMapView coords={secilenSehir} />
          <TileLayer
            url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
            attribution="Kültür360"
          />

          {konum && (
            <Marker position={[konum.latitude, konum.longitude]}>
              <Popup>📍 Şu an buradasınız</Popup>
            </Marker>
          )}

          {filtrelenmisYerler.map((yer) => (
            <Marker key={yer.id} position={[yer.latitude, yer.longitude]}>
              <Popup maxWidth={300}>
                <strong>{yer.isim}</strong>
                <br />
                {yer.aciklama}
                <br />
                <em>{yer.sehir}</em>
                <br />
                <img
                  src={`http://localhost:5025${yer.fotografUrl}`}
                  alt={yer.isim}
                  className="img-fluid mt-2"
                  style={{ width: "100px", borderRadius: "6px" }}
                />
              </Popup>
            </Marker>
          ))}
        </MapContainer>
      </div>
    </>
  );
}
export default App;
