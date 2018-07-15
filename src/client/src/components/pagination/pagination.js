import React, { Component } from "react";
import styled from "emotion/react";

import PaginationPrev from "./pagination_prev";
import PaginationNext from "./pagination_next";

const PaginationLayout = styled.div`
  display: flex;
  justify-content: space-between;
  margin: 0;
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
