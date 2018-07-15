import React, { Component } from "react";
import { connect } from "react-redux";
import Button from "../misc/button";
import {paginationPrev} from "../../actions/pagination";

class PaginationNext extends Component {
  constructor(props) {
    super(props);

    this.onClickWrapper = this.onClickWrapper.bind(this);
  }

  onClickWrapper() {
    this.props.onClick();
  }

  render() {
    return (
      <div onClick = {this.onClickWrapper}>
        <Button>
          Назад
        </Button>
      </div>
    )
  }
}

const mapStateToProps = state => ({
  activeCardNumber: state.cards.activeCardNumber,
  paginationNext: state.cards.paginationNext
});

const mapDispatchToProps = dispatch => ({
  onClick: () => dispatch(paginationPrev())
});

export default connect(
  mapStateToProps,
  mapDispatchToProps
)(PaginationNext);
