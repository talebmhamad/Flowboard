export default function Loader({

  text = "Loading...",

  fullScreen = false

}) {

  return (

    <div
      className="
        d-flex
        flex-column
        justify-content-center
        align-items-center
        gap-2
      "
      style={{
        height: fullScreen
          ? "100vh"
          : "50vh"
      }}
    >

      <div
        className="spinner-border text-primary"
        role="status"
      >
        <span className="visually-hidden">
          Loading...
        </span>
      </div>

      <span className="text-muted small">
        {text}
      </span>

    </div>
  );
}