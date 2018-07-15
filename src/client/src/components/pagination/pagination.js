import React, { Component } from "react";
import styled from "emotion/react";

import PaginationPrev from "./pagination_prev";
import PaginationNext from "./pagination_next";

const PaginationLayout = styled.div`
  display: flex;
  justify-content: space-between;
  width: 620px;
  margin: 0 15px;
`;

class Pagination extends Component {
  render() {
    return (
      <PaginationLayout>
        <PaginationPrev/>
        <PaginationNext/>
      </PaginationLayout>
    )
  }
}
export default Pagination;
