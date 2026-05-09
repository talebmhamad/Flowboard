export default function Loader({ text = "Loading..." }) {
  return (
    <div
      className="d-flex flex-column justify-content-center align-items-center gap-2"
      style={{ height: "50vh" }}
    >
      <div className="spinner-border text-primary" role="status"></div>
      <span className="text-muted small">{text}</span>
    </div>
  );
}