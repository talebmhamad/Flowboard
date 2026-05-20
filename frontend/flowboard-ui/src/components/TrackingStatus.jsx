import React from "react";

export default function TrackingStatus({
  task
}) {

  let currentStep =
    "Under Review";

  if (
    task?.status === "Draft"
  ) {

    currentStep = "Draft";
  }
  else if (
    task?.status === "Approved"
  ) {

    currentStep = "Approved";
  }

  const steps = [
    "Draft",
    "Submitted",
    "Under Review",
    "Approved",
  ];

  const activeIndex =
    steps.indexOf(currentStep);

  return (

    <div className="container-fluid">

      {/* TOP CARDS */}

      <div className="row g-3 mb-5">

        <div className="col-md-3">

          <div className="card border-0 shadow-sm rounded-4 h-100">

            <div className="card-body">

              <small className="text-muted fw-bold">
                REFERENCE NUMBER
              </small>

              <h6 className="mt-2 mb-0">

                {
                  task?.referenceNumber
                }

              </h6>

            </div>

          </div>

        </div>

        <div className="col-md-3">

          <div className="card border-0 shadow-sm rounded-4 h-100">

            <div className="card-body">

              <small className="text-muted fw-bold">
                DOCUMENT TYPE
              </small>

              <h6 className="mt-2 mb-0">

                {
                  task?.documentType
                }

              </h6>

            </div>

          </div>

        </div>

        <div className="col-md-3">

          <div className="card border-0 shadow-sm rounded-4 h-100">

            <div className="card-body">

              <small className="text-muted fw-bold">
                CREATED DATE
              </small>

              <h6 className="mt-2 mb-0">

                {
                  task?.createdDate
                    ? new Date(
                        task.createdDate
                      )
                        .toLocaleDateString()
                    : "-"
                }

              </h6>

            </div>

          </div>

        </div>

        <div className="col-md-3">

          <div className="card border-0 shadow-sm rounded-4 h-100">

            <div className="card-body">

              <small className="text-muted fw-bold">
                STATUS
              </small>

              <div className="mt-2">

                <span className="badge bg-success rounded-pill px-3 py-2">

                  {
                    task?.status ||
                    "Pending"
                  }

                </span>

              </div>

            </div>

          </div>

        </div>

      </div>

      {/* STATUS TITLE */}

      <div className="mb-4">

        <h5 className="fw-bold">
          Status
        </h5>

        <span className="text-muted">

          {
            task?.referenceNumber
          }

        </span>

      </div>

      {/* TIMELINE */}

      <div className="d-flex justify-content-between align-items-center position-relative mt-5">

        {
          steps.map(
            (
              step,
              index
            ) => (

            <div
              key={step}
              className="flex-fill text-center position-relative"
            >

              {/* LINE */}

              {
                index <
                  steps.length - 1 && (

                  <div
                    className={`position-absolute top-50 start-50 translate-middle-y w-100 ${
                      index <
                      activeIndex
                        ? "bg-success"
                        : "bg-light"
                    }`}
                    style={{
                      height: "3px",
                      zIndex: 1,
                    }}
                  />

                )
              }

              {/* CIRCLE */}

              <div
                className={`mx-auto rounded-circle d-flex align-items-center justify-content-center ${
                  index <= activeIndex
                    ? "bg-success text-white border-success"
                    : "bg-white text-muted border-light"
                }`}
                style={{
                  width: "55px",
                  height: "55px",
                  borderWidth: "3px",
                  borderStyle: "solid",
                  position: "relative",
                  zIndex: 2,
                }}
              >

                <i className="bi bi-check-lg"></i>

              </div>

              {/* LABEL */}

              <div className="mt-3">

                <span className="fw-medium">

                  {step}

                </span>

              </div>

            </div>
          ))
        }

      </div>

    </div>
  );
}