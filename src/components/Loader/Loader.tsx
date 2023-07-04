import React from "react";

const Loader: React.FC = () => {
  return (
    // <div>
    //     <h3>Loading ... </h3>
    // </div>

    <div className="overlay">
      <div className="overlay__inner">
        <div className="overlay__content">
          <span className="spinner"></span>
        </div>
      </div>
    </div>
  );
};
export default Loader;
