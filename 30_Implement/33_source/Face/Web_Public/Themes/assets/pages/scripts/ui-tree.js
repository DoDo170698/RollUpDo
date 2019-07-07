var UITree = function () {

    var handleSample1 = function () {

        $('#tree_1').jstree({
            "core" : {
                "themes" : {
                    "responsive": false
                }            
            },
            "types" : {
                "default" : {
                    "icon" : "fa fa-folder icon-state-warning icon-lg"
                },
                "file" : {
                    "icon" : "fa fa-file icon-state-warning icon-lg"
                }
            },
            "plugins": ["types"]
        });

        // handle link clicks in tree nodes(support target="_blank" as well)
        $('#tree_1').on('select_node.jstree', function(e,data) { 
            var link = $('#' + data.selected).find('a');
            if (link.attr("href") != "#" && link.attr("href") != "javascript:;" && link.attr("href") != "") {
                if (link.attr("target") == "_blank") {
                    link.attr("href").target = "_blank";
                }
                document.location.href = link.attr("href");
                return false;
            }
        });
    }

    var handleSample2 = function () {
        $('#tree_2').jstree({
            'plugins': ["wholerow", "checkbox", "types"],
            'core': {
                "themes" : {
                    "responsive": false
                },    
                'data': [{
                        "text": "Quyền lợi bảo hiểm",
                        "children": [{
                            "text": "<lable>Trường hợp tử vong hoặc thương tật toàn bộ vĩnh viễn</lable>",
                            "state": {
                                "selected": true
                            }
                        }, {
                            "text": "Trường hợp thương tật bộ phận vĩnh viễn: Chi trả theo tỷ lệ thương tật và số tiền bảo hiểm (100% trường hợp tử vong)",
                            "icon": "fa fa-warning icon-state-danger"
                        }, {
                            "text": "Chi phí y tế: PTI sẽ chi trả theo chi phí y tế thực tế, hợp lý theo giới hạn số tiền bảo hiểm lựa chọn của Người được bảo hiểm;",
                            "icon" : "fa fa-folder icon-state-default",
                            "state": {
                                "opened": true
                            }
                        }, {
                            "text": "Trợ cấp mất giảm thu nhâp (lương) hoặc trợ cấp ngày trong thời gian điều trị",
                            "icon": "fa fa-warning icon-state-warning"
                        }, {
                            "text": "disabled node",
                            "icon": "fa fa-check icon-state-success",
                            "state": {
                                "disabled": true
                            }
                        }]
                    },
                    {
                        "text": "Quyền lợi bảo hiểm2",
                        "children": [{
                            "text": "Điều trj ngoại chú",
                            "state": {
                                "selected": true
                            }
                        }, {
                            "text": "điều trị nội chú",
                            "icon": "fa fa-warning icon-state-warning",
                            "children": [{
                            "text": "<lable>Chi phí 1</lable>",
                            "state": {
                                "selected": true
                            }
                        }, {
                            "text": "chi phí 2 (100% trường hợp tử vong)",
                            "icon": "fa fa-warning icon-state-danger"
                        }
                        ]
                        }
                        ]
                    }
                   
                ]
            },
            "types" : {
                "default" : {
                    "icon" : "fa fa-folder icon-state-warning icon-lg"
                },
                "file" : {
                    "icon" : "fa fa-file icon-state-warning icon-lg"
                }
            }
        });
    }

    var contextualMenuSample = function() {

        $("#tree_3").jstree({
            "core" : {
                "themes" : {
                    "responsive": false
                }, 
                // so that create works
                "check_callback" : true,
                'data': [{
                        "text": "Parent Node",
                        "children": [{
                            "text": "Initially selected",
                            "state": {
                                "selected": true
                            }
                        }, {
                            "text": "Custom Icon",
                            "icon": "fa fa-warning icon-state-danger"
                        }, {
                            "text": "Initially open",
                            "icon" : "fa fa-folder icon-state-success",
                            "state": {
                                "opened": true
                            },
                            "children": [
                                {"text": "Another node", "icon" : "fa fa-file icon-state-warning"}
                            ]
                        }, {
                            "text": "Another Custom Icon",
                            "icon": "fa fa-warning icon-state-warning"
                        }, {
                            "text": "Disabled Node",
                            "icon": "fa fa-check icon-state-success",
                            "state": {
                                "disabled": true
                            }
                        }, {
                            "text": "Sub Nodes",
                            "icon": "fa fa-folder icon-state-danger",
                            "children": [
                                {"text": "Item 1", "icon" : "fa fa-file icon-state-warning"},
                                {"text": "Item 2", "icon" : "fa fa-file icon-state-success"},
                                {"text": "Item 3", "icon" : "fa fa-file icon-state-default"},
                                {"text": "Item 4", "icon" : "fa fa-file icon-state-danger"},
                                {"text": "Item 5", "icon" : "fa fa-file icon-state-info"}
                            ]
                        }]
                    },
                    "Another Node"
                ]
            },
            "types" : {
                "default" : {
                    "icon" : "fa fa-folder icon-state-warning icon-lg"
                },
                "file" : {
                    "icon" : "fa fa-file icon-state-warning icon-lg"
                }
            },
            "state" : { "key" : "demo2" },
            "plugins" : [ "contextmenu", "dnd", "state", "types" ]
        });
    
    }

     var ajaxTreeSample = function() {

        $("#tree_4").jstree({
            "core" : {
                "themes" : {
                    "responsive": false
                }, 
                // so that create works
                "check_callback" : true,
                'data' : {
                    'url' : function (node) {
                      return '../demo/jstree_ajax_data.php';
                    },
                    'data' : function (node) {
                      return { 'parent' : node.id };
                    }
                }
            },
            "types" : {
                "default" : {
                    "icon" : "fa fa-folder icon-state-warning icon-lg"
                },
                "file" : {
                    "icon" : "fa fa-file icon-state-warning icon-lg"
                }
            },
            "state" : { "key" : "demo3" },
            "plugins" : [ "dnd", "state", "types" ]
        });
    
    }


    return {
        //main function to initiate the module
        init: function () {

            handleSample1();
            handleSample2();
            contextualMenuSample();
            ajaxTreeSample();

        }

    };

}();

if (App.isAngularJsApp() === false) {
    jQuery(document).ready(function() {    
       UITree.init();
    });
}